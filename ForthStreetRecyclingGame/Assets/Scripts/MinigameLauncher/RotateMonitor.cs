/*
 * File: RotateMonitor.cs
 * Purpose: Rotate the monitor in the main scene
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using System.Collections;
using UnityEngine;

/// <summary>
/// This class is used to rotate the monitor in the main scene.
/// It has methods to rotate to and from the given angles.
/// You can also set the speed of the rotation.
/// </summary>
/// <remarks>
/// I'm not thrilled about switching the camera and resetting minigame here, but these need to happen after the monitor rotation coroutines.
/// Eventually the coroutines could be refactored to take a callback function to call after the rotation is complete
/// </remarks>
public class RotateMonitor : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // The speed at which the monitor rotates
    public CameraSwitcher cameraSwitcher; // Reference to the CameraSwitcher script
    public int rotateStart = 120; // The starting rotation of the monitor
    public int rotateTarget = 0; // The target rotation of the monitor
    public TestMinigameManager testMinigameManager; // Reference to the TestMinigameManager script
    public bool isRotating = false; // Flag to check if the monitor is currently rotating

    /// <summary>
    /// Set the starting rotation of the monitor
    /// </summary>
    void Start()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rotateStart);
    }

    /// <summary>
    /// Rotate the monitor to the target rotation
    /// The camera is switched to the minigame camera after rotating in the coroutine
    /// </summary>
    public void RotateToTarget()
    {
        StartCoroutine(RotateToTargetCoroutine());
    }

    /// <summary>
    /// Switch camera to main and rotate the monitor to the starting rotation
    /// </summary>
    public void RotateToStart()
    {
        // Switch back to the main camera before rotating
        cameraSwitcher.SwitchToMainCamera();
        StartCoroutine(RotateToStartCoroutine());
    }

    /// <summary>
    /// Coroutine to rotate the monitor to the target rotation
    /// </summary>
    private IEnumerator RotateToTargetCoroutine()
    {
        isRotating = true;
        while (transform.rotation.eulerAngles.z > rotateTarget)
        {
            // Decrease the z rotation
            float zRotation = transform.rotation.eulerAngles.z - rotationSpeed * Time.deltaTime;
            // Ensure the z rotation is not less than rotateTarget
            zRotation = Mathf.Max(zRotation, rotateTarget);
            // Set the new rotation
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, zRotation);

            yield return null;
        }
        
        isRotating = false;
        // Switch to the minigame camera after rotating
        cameraSwitcher.SwitchToMinigameCamera();
    }

    /// <summary>
    /// Coroutine to rotate the monitor to the starting rotation
    /// </summary>
    private IEnumerator RotateToStartCoroutine()
    {
        isRotating = true;
        while (transform.rotation.eulerAngles.z < rotateStart)
        {
            // Increase the z rotation
            float zRotation = transform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime;
            // Ensure the z rotation is not more than rotateStart
            zRotation = Mathf.Min(zRotation, rotateStart);
            // Set the new rotation
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, zRotation);

            yield return null;
        }

        isRotating = false;
        // reset the minigame manager
        testMinigameManager.ResetGame();
    }
}

