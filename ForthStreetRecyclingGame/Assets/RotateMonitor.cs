using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to rotate the monitor in the main scene.
/// It has methods to rotate from 270 to 0 degrees and from 0 to 270 degrees on the z-axis.
/// You can also set the speed of the rotation.
/// </summary>
public class RotateMonitor : MonoBehaviour
{
    // The speed of the rotation
    public float rotationSpeed = 1.0f;

    // The target rotation
    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        // Set the target rotation to the current rotation
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the monitor to the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Rotate the monitor from 270 to 0 degrees on the z-axis.
    /// </summary>
    public void RotateToZero()
    {
        targetRotation = Quaternion.Euler(0, 0, 0);
    }

    /// <summary>
    /// Rotate the monitor from 0 to 270 degrees on the z-axis.
    /// </summary>
    public void RotateTo270()
    {
        targetRotation = Quaternion.Euler(0, 0, 270);
    }
}

