/*
 * File: MinigameSelector.cs
 * Purpose: Creates a UI element displaying FPS
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot, based on code from https://www.sharpcoderblog.com/blog/unity-fps-counter
 */

using UnityEngine;
using TMPro;

/// <summary>
/// This class is used to display the FPS in the game
/// </summary>
/// <remarks>
/// While this can be useful for determining the impact of the various game objects on performance,
/// its important to remember that other factors, such as the size of the scene view window and 
/// the resolution of the game view window, can influence this.
/// </remarks>
public class FPSCounter : MonoBehaviour
{
    public TMP_Text fpsText;    // The TextMeshPro text field to display the FPS value
    public float updateInterval = 0.5f; // The update interval in seconds

    private float accum = 0.0f; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Remaining time for current interval

    void Start()
    {
        timeleft = updateInterval;
    }

    /// <summary>
    /// Every frame, calculate the FPS and update the TextMeshPro text field
    /// </summary>
    void Update()
    {
        // Subtract the time since last frame from the remaining time
        timeleft -= Time.deltaTime;

        // Add the scaled time since last frame to the accumulated time
        // Divide Time.timeScale by Time.deltaTime to get the number of frames that could be rendered in one second
        accum += Time.timeScale / Time.deltaTime;

        // Increment the frame count
        ++frames;

        // Check if the time left is less than or equal to zero
        if (timeleft <= 0.0)
        {
            // Calculate the average FPS over the interval
            float fps = accum / frames;

            // Format the FPS value to 2 decimal places
            string format = string.Format("{0:F2} FPS", fps);

            // Update the TextMeshPro text field with the formatted FPS value
            fpsText.text = format;

            // Change the color of the text based on the FPS value
            if (fps < 10)
            {
                fpsText.color = Color.red;
            }
            else if (fps < 30)
            {
                fpsText.color = Color.yellow;
            }
            else
            {
                fpsText.color = Color.green;
            }

            // Reset values for the next interval
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}
