/*
 * File: Rotate.cs
 * Purpose: Basic rotation script for objects
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// This class is used to rotate an object.
/// </summary>
/// <remarks>
/// This should be refactored to work on a provided axis.
/// </remarks>
public class Rotate : MonoBehaviour
{
    public float RotationSpeed = 50f;

    /// <summary>
    /// Perform the rotation
    /// </summary>
    void Update()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }
}