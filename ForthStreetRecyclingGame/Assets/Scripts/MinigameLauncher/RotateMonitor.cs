/*
 * File: RotateMonitor.cs
 * Purpose: Rotate the monitor in the main scene
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This class is used to rotate the monitor in the main scene.
/// It has methods to rotate to and from the given angles.
/// You can also set the speed of the rotation.
/// </summary>
/// <todo>
/// The coroutines could be refactored to use a single coroutine with a target angle parameter.
/// </todo>
public class RotateMonitor : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // The speed at which the monitor rotates
    public CameraSwitcher cameraSwitcher; // Reference to the CameraSwitcher script
    public int rotateStart = 120; // The starting rotation of the monitor
    public int rotateTarget = 0; // The target rotation of the monitor
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
    /// </summary>
    /// <param name="afterRotateCallbacks">Callbacks to call after the rotation is complete</param>
    public void RotateToTarget(Action[] afterRotateCallbacks = null)
    {
        if (isRotating) { return; }
        SFXManager.Instance.Play("MonitorDown");
        StartCoroutine(RotateToTargetCoroutine(afterRotateCallbacks));
    }

    /// <summary>
    /// Rotate the monitor to the starting position
    /// </summary>
    /// <param name="afterRotateCallbacks">Callbacks to call after the rotation is complete</param>
    public void RotateToStart(Action[] afterRotateCallbacks = null)
    {
        if (isRotating) { return; }
        SFXManager.Instance.Play("MonitorUp");
        // Switch back to the main camera before rotating
        // cameraSwitcher.SwitchToMainCamera();
        StartCoroutine(RotateToStartCoroutine(afterRotateCallbacks));
    }

    /// <summary>
    /// Coroutine to rotate the monitor to the target rotation
    /// </summary>
    /// <param name="afterRotateCallbacks">Callbacks to call after the rotation is complete</param>
    private IEnumerator RotateToTargetCoroutine(Action[] afterRotateCallbacks = null)
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
        if (afterRotateCallbacks != null)
        {
            foreach (var callback in afterRotateCallbacks)
            {
                callback();
            }
        }
    }

    /// <summary>
    /// Coroutine to rotate the monitor to the starting rotation
    /// </summary>
    /// <param name="afterRotateCallbacks">Callbacks to call after the rotation is complete</param>
    private IEnumerator RotateToStartCoroutine(Action[] afterRotateCallbacks = null)
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
        if (afterRotateCallbacks != null)
        {
            foreach (var callback in afterRotateCallbacks)
            {
                callback();
            }
        }
    }
}

