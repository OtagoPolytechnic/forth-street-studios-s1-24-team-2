using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to switch between the main scene camera and the minigame camera.
/// </summary>
public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera minigameCamera;

    // minigame camera output texture
    private RenderTexture minigameOutputTexture;

    void Start()
    {
        // store the minigame camera output texture
        minigameOutputTexture = minigameCamera.targetTexture;
    }


    public void SwitchToMinigameCamera()
    {
        mainCamera.enabled = false;
        // minigame camera switch output texture to none
        minigameCamera.targetTexture = null;
        //minigameCamera.enabled = true;
    }

    public void SwitchToMainCamera()
    {
        // minigame camera switch output texture back to the original
        minigameCamera.targetTexture = minigameOutputTexture;
        mainCamera.enabled = true;
        //minigameCamera.enabled = false;
    }
}
