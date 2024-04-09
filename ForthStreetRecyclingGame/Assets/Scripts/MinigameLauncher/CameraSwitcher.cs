/*
 * File: RotateMonitor.cs
 * Purpose: Rotate the monitor in the main scene
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// This class is used to switch between the main scene camera and the minigame camera.
/// </summary>
/// <remarks>
/// Once multiple minigames are implemented, this class will need to be refactored to handle multiple cameras.
/// </remarks>
public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera minigameCamera;

    private RenderTexture minigameOutputTexture; // minigame camera output texture

    void Start()
    {
        // initialize the minigame camera output texture
       minigameOutputTexture = minigameCamera.targetTexture;
    }

    /// <summary>
    /// Switch to the minigame camera
    /// This is done by disabling the main camera and removing the output texture from the minigame camera
    /// </summary>
    public void SwitchToMinigameCamera()
    {
        mainCamera.enabled = false;
        // minigame camera switch output texture to none
        minigameCamera.targetTexture = null;
    }

    /// <summary>
    /// Switch to the main camera
    /// This is done by enabling the main camera and setting the output texture of the minigame camera back to the original
    /// </summary>
    public void SwitchToMainCamera()
    {
        // minigame camera switch output texture back to the original
        minigameCamera.targetTexture = minigameOutputTexture;
        mainCamera.enabled = true;
    }
}
