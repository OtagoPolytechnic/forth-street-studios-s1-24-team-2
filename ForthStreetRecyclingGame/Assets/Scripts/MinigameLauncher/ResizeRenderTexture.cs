/*
 * File: ResizeRenderTexture.cs
 * Purpose: Resizes a RenderTexture to match the current screen resolution
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// Resizes a RenderTexture to match the current screen resolution
/// This is because the RenderTexture needs to match the screen size 
/// in order for overlay UI elements to be displayed correctly on the monitor
/// </summary>
public class ResizeRenderTexture : MonoBehaviour
{
    public RenderTexture renderTexture; // The RenderTexture to resize

    private Vector2 previousResolution;
    private Vector2Int originalTextureSize;

    /// <summary>
    /// Initialise values and set the texture size to match the current screen resolution
    /// </summary>
    void Start()
    {
        originalTextureSize = new Vector2Int(renderTexture.width, renderTexture.height);
        previousResolution = new Vector2(Screen.width, Screen.height);
        ResizeTexture(Screen.width, Screen.height);
    }

    /// <summary>
    /// Check if the screen resolution has changed and resize the RenderTexture if necessary
    /// </summary>
    void Update()
    {
        // Check if the screen resolution has changed
        Vector2 currentResolution = new Vector2(Screen.width, Screen.height);
        if (currentResolution != previousResolution)
        {
            // Resize the RenderTexture
            ResizeTexture(Screen.width, Screen.height);
            previousResolution = currentResolution;
        }
    }

    /// <summary>
    /// Resizes the RenderTexture to the specified width and height
    /// </summary>
    /// <param name="width">The new width of the RenderTexture</param>
    /// <param name="height">The new height of the RenderTexture</param>
    public void ResizeTexture(int width, int height)
    {
        // Release the current RenderTexture
        renderTexture.Release();
        // Change the size to match the current screen size
        renderTexture.width = width;
        renderTexture.height = height;
        // Reinitialize the RenderTexture
        renderTexture.Create();
    }

    /// <summary>
    /// Resets the RenderTexture to its original size when the application is closed
    /// This is so changes to the RenderTexture are not constantly being pushed to the Git repository
    /// </summary>
    void OnApplicationQuit()
    {
        ResizeTexture(originalTextureSize.x, originalTextureSize.y);
    }
}

