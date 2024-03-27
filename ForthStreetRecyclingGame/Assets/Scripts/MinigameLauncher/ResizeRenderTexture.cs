using UnityEngine;

public class ResizeRenderTexture : MonoBehaviour
{
    private Vector2 previousResolution;
    private Vector2 originalTextureSize;
    public RenderTexture renderTexture; // The RenderTexture to resize

        void Start()
    {
        originalTextureSize = new Vector2(renderTexture.width, renderTexture.height);
        previousResolution = new Vector2(Screen.width, Screen.height);
    }

        void Update()
    {
        Vector2 currentResolution = new Vector2(Screen.width, Screen.height);
        if (currentResolution != previousResolution)
        {
            ResizeTexture(Screen.width, Screen.height);
            previousResolution = currentResolution;
        }
    }

    public void ResizeTexture(int width, int height)
    {
        // Release the current RenderTexture
        renderTexture.Release();
        // Change the size to match the current screen size
        renderTexture.width = Screen.width;
        renderTexture.height = Screen.height;
        // Reinitialize the RenderTexture
        renderTexture.Create();
    }

    private void onDisable()
    {
        // Reset the RenderTexture size to the original size
        renderTexture.width = (int)originalTextureSize.x;
        renderTexture.height = (int)originalTextureSize.y;
    }
}

