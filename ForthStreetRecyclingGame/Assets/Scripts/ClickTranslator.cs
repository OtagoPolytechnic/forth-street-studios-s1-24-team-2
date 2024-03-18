using UnityEngine;
using UnityEngine.EventSystems;

public class ClickTranslator : MonoBehaviour
{
    public Camera mainCamera;
    private Camera minigameCamera;
    private RectTransform canvasOverlay;

    void Start()
    {

    }

    public void AssignCameraAndCanvas()
    {
        // Find the camera by name
        minigameCamera = GameObject.Find("MinigameCamera").GetComponent<Camera>();
        if (minigameCamera == null)
        {
            Debug.LogError("Camera not found!");
        }

        // Find the canvas by name
        GameObject minigameCanvasGO = GameObject.Find("MinigameCanvas");
        if (minigameCanvasGO != null)
        {
            canvasOverlay = minigameCanvasGO.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("MinigameCanvas not found!");
        }

        if (canvasOverlay == null)
        {
            Debug.LogError("Canvas not found!");
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Step 1: Get click position on the raw image
            Vector3 clickPositionRawImage = Input.mousePosition;

            // Step 2: Convert click position from raw image's screen space to world space
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(clickPositionRawImage.x, clickPositionRawImage.y, mainCamera.nearClipPlane));

            // Step 3: Convert world space position to screen space on the minigame camera
            Vector3 screenPositionMinigameCamera = minigameCamera.WorldToScreenPoint(worldPosition);

            // Step 4: Convert screen space position to local space on the canvas overlay
            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasOverlay, screenPositionMinigameCamera, minigameCamera, out localPosition);

            // Now 'localPosition' contains the position of the click on the canvas overlay
            // You can handle the click here
            // log the actual click position and the translated click position
            Debug.Log("Click position: " + clickPositionRawImage + " Translated click position: " + localPosition);
        }
    }
}
