using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WindowedSceneRenderer : MonoBehaviour
{
    public string sceneToRenderName; // Name of the scene to render

    // reference to the secondary canvas
    public Canvas secondaryCanvas;

    // reference to the raw image
    public RawImage rawImage;

    // reference to the render texture
    private RenderTexture renderTexture;

    
    void Start()
    {
        // Load the scene asynchronously
        SceneManager.LoadSceneAsync(sceneToRenderName, LoadSceneMode.Additive);
    }

    void Update()
    {
        // Find the loaded scene by its name
        Scene sceneToRender = SceneManager.GetSceneByName(sceneToRenderName);

        // Check if the scene is valid and ready to be rendered
        if (sceneToRender.IsValid() && sceneToRender.isLoaded)
        {
            // Get all GameObjects in the scene
            GameObject[] rootObjects = sceneToRender.GetRootGameObjects();

            // Loop through each GameObject
            foreach (GameObject go in rootObjects)
            {
                // Find the camera in the scene
                Camera sceneCamera = go.GetComponent<Camera>();

                // Check if the GameObject has a camera component
                if (sceneCamera != null)
                {
                    // Ensure the camera is rendering to a Render Texture
                    if (sceneCamera.targetTexture != null)
                    {
                        renderTexture = sceneCamera.targetTexture;
                        rawImage.texture = renderTexture;
                        break;
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        // Release the Render Texture when the GameObject is destroyed
        if (renderTexture != null)
        {
            renderTexture.Release();
        }
    }
}