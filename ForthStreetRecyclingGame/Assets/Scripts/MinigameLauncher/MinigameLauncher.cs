/*
 * File: MinigameLauncher.cs
 * Purpose: Launching minigames and returning to the main scene after the minigame is completed
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

/// <summary>
/// This class is used to launch the minigame and return to the main scene after the minigame is completed.
/// </summary>
public class MinigameLauncher : MonoBehaviour
{
    public RotateMonitor rotateMonitor; // Reference to the RotateMonitor script, should be assigned in the inspector
    // this a new way of using autoprops I've discovered. It let's you expose the backing field to the inspector while still using a property
    [field: SerializeField] public float GameOverDelay { get; private set; } = 2.5f; // The delay before the game over event is fired
    private CameraSwitcher cameraSwitcher;  // Reference to the CameraSwitcher script
    public Minigame currentMinigame;   // Reference to the current minigame
    public UnityEvent<bool> minigameOver;    // Event that is fired when the minigame is over
    private MinigameObjectManager minigameObjectManager;  // Reference to the MinigameObjectManager script
    public UnityEvent<string> switchingMinigame;    // Event fired when a minigame is launched

    [SerializeField] private ItemSpawner itemSpawner; //Pauses objects on minigame loading
    [SerializeField] private ConveyorManager conveyorManager; //Pauses objects on minigame loading 

    // Countdown panel and text references
    [SerializeField] public GameObject countdownPanel;
    [SerializeField] private TMP_Text countdownText;
    private const int COUNTDOWN_TIME = 3;

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
        minigameObjectManager = MinigameObjectManager.instance;
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
        switchingMinigame.Invoke(currentMinigame.minigameName);
        cameraSwitcher.EnableMinigameCamera(isEnabled: true);
        minigameObjectManager.SetActive(currentMinigame, active:true);

        // These callbacks are called after the monitor has rotated
        System.Action[] afterRotateCallbacks = new System.Action[]
        {
            () => StartCoroutine(CountdownCoroutine()) //Only start countdown once rotation is complete
        };

        // Rotate monitor in front of main camera
        countdownPanel.SetActive(true);
        rotateMonitor.RotateToTarget(afterRotateCallbacks);

    }

    /// <summary>
    /// Show a panel for a 3 second countdown before starting the minigame
    /// </summary>
    /// <returns>Wait to update current countdown value over time (i.e 3, 2, 1)</returns>
    public IEnumerator CountdownCoroutine()
    {   
        for (int i = COUNTDOWN_TIME; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownPanel.SetActive(false);
        countdownText.text = COUNTDOWN_TIME.ToString(); //Reset the countdown value for next time monitor rotates

        // Start the minigame after the countdown
        cameraSwitcher.SwitchToMinigameCamera();
        currentMinigame.MinigameBegin();
    }

    /// <summary>
    /// Set the minigame and launch it
    /// </summary>
    /// <param name="minigame">The minigame to be set as the current minigame</param>
    public void LaunchMinigame(Minigame minigame)
    {
        SetMinigame(minigame);
        LaunchMinigame();
        itemSpawner.minigame = true;
        conveyorManager.minigame = true;
    }

    /// <summary>
    /// Handle the game over event
    /// Switch back to the main camera and reset the minigame
    /// </summary>
    /// <param name="success">Whether the player has won the minigame</param>
    private void HandleGameOver(bool success = false)
    {
        switchingMinigame.Invoke("SortingFacility");
        cameraSwitcher.SwitchToMainCamera();
        // These callbacks are called after the monitor has rotated
        System.Action[] afterRotateCallbacks = new System.Action[]
        {
            currentMinigame.Reset,
            // Disable the minigame camera so it doesn't mess up the DragObject script
            () => cameraSwitcher.EnableMinigameCamera(isEnabled: false),
            // fire an event with the success bool after the monitor has rotated
            () => minigameOver.Invoke(success),
            () => minigameObjectManager.SetActive(currentMinigame, active:false)
        };
        // Rotate monitor back to starting position
        rotateMonitor.RotateToStart(afterRotateCallbacks);
        itemSpawner.minigame = false;
        conveyorManager.minigame = false;
    }
}
