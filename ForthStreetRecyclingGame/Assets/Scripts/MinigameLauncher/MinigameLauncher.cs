/*
 * File: MinigameLauncher.cs
 * Purpose: Launching minigames and returning to the main scene after the minigame is completed
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used to launch the minigame and return to the main scene after the minigame is completed.
/// </summary>
public class MinigameLauncher : MonoBehaviour
{
    public RotateMonitor rotateMonitor; // Reference to the RotateMonitor script, should be assigned in the inspector
    private CameraSwitcher cameraSwitcher;  // Reference to the CameraSwitcher script
    public Minigame currentMinigame;   // Reference to the current minigame
    public UnityEvent<bool> minigameOver;    // Event that is fired when the minigame is over


    #region Singleton
    // Singleton pattern
    public static MinigameLauncher instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    /// <summary>
    /// Initialise CameraSwitcher
    /// If currentMinigame is not null, set the minigame camera to the current minigame's camera
    /// and subscribe to the OnGameOver event
    /// </summary>
    void Start()
    {
        cameraSwitcher = CameraSwitcher.instance;
        if (currentMinigame != null)
        {
            cameraSwitcher.MinigameCamera = currentMinigame.minigameCamera;
            currentMinigame.OnGameOver.AddListener(HandleGameOver);
        }
    }

    /// <summary>
    /// Set the current minigame, assign the camera and subscribe to the OnGameOver event
    /// </summary>
    /// <param name="minigame">The minigame to be set as the current minigame</param>
    public void SetMinigame(Minigame minigame)
    {
        if (minigame == null)
        {
            currentMinigame = null;
            return;
        }
        // disable minigame camera
        if (currentMinigame != null)
        {
            currentMinigame.minigameCamera.enabled = false;
        }
        currentMinigame = minigame;
        cameraSwitcher.MinigameCamera = currentMinigame.minigameCamera;
        currentMinigame.OnGameOver.AddListener(HandleGameOver);
    }

    /// <summary>
    /// Launch the current minigame
    /// Rotate the monitor in front of the main camera and switch to the minigame camera
    /// </summary>
    public void LaunchMinigame()
    {
        if (currentMinigame == null) return;
        // These callbacks are called after the monitor has rotated
        System.Action[] afterRotateCallbacks = new System.Action[]
        {
            cameraSwitcher.SwitchToMinigameCamera,
            currentMinigame.MinigameBegin
        };
        // Rotate monitor in front of main camera
        rotateMonitor.RotateToTarget(afterRotateCallbacks);
    }

    /// <summary>
    /// Set the minigame and launch it
    /// </summary>
    /// <param name="minigame">The minigame to be set as the current minigame</param>
    public void LaunchMinigame(Minigame minigame)
    {
        SetMinigame(minigame);
        LaunchMinigame();
    }

    /// <summary>
    /// Handle the game over event
    /// Switch back to the main camera and reset the minigame
    /// </summary>
    /// <param name="success">Whether the player has won the minigame</param>
    private void HandleGameOver(bool success = false)
    {
        cameraSwitcher.SwitchToMainCamera();
        // These callbacks are called after the monitor has rotated
        System.Action[] afterRotateCallbacks = new System.Action[]
        {
            currentMinigame.Reset,
            // Disable the minigame camera so it doesn't mess up the DragObject script
            cameraSwitcher.DisableMinigameCamera,
            // fire an event with the success bool after the monitor has rotated
            () => minigameOver.Invoke(success)
        };
        // Rotate monitor back to starting position
        rotateMonitor.RotateToStart(afterRotateCallbacks);
    }
}
