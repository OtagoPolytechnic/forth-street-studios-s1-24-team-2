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
    public float rotationSpeed = 100.0f;

    public CameraSwitcher CameraSwitcher;

    // The target rotation
    private Quaternion targetRotation;

    public int RotateA = 0;
    public int RotateB = 180;

    // Start is called before the first frame update
    private void Start()
    {
        // Set the target rotation to the current rotation
        targetRotation = transform.rotation;
    }

    public void RotateAtoB()
    {
        StartCoroutine(rotateAtoBCoroutine());
    }


    public void RotateBtoA()
    {
        CameraSwitcher.SwitchToMainCamera();
        StartCoroutine(rotateBtoACoroutine());
    }

    private IEnumerator rotateAtoBCoroutine()
    {
        while (transform.rotation.eulerAngles.z > RotateB)
        {
            // Decrease the z rotation
            float zRotation = transform.rotation.eulerAngles.z - rotationSpeed * Time.deltaTime;
            // Ensure the z rotation is not less than 
            zRotation = Mathf.Max(zRotation, RotateB);
            // Set the new rotation
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, zRotation);

            yield return null;
        }

        CameraSwitcher.SwitchToMinigameCamera();
    }

    private IEnumerator rotateBtoACoroutine()
    {
        while (transform.rotation.eulerAngles.z < RotateA)
        {
            // Increase the z rotation
            float zRotation = transform.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime;
            // Ensure the z rotation is not more than 270
            zRotation = Mathf.Min(zRotation, RotateA);
            // Set the new rotation
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, zRotation);

            yield return null;
        }
    }
}

