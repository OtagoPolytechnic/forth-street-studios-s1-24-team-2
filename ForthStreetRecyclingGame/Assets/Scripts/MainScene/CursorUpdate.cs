using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D clickCursor;
    private Vector2 cursorHotspotDefault;
    private Vector2 cursorHotspotClick;

    private void Start()
    {
        // Set the cursor hotspot to the center of the textures
        cursorHotspotDefault = new Vector2(defaultCursor.width / 2, defaultCursor.height / 2);
        cursorHotspotClick = new Vector2(clickCursor.width / 2, clickCursor.height / 2);

        // Set the default cursor initially
        SetDefaultCursor();
    }

    private void Update()
    {
        // Check if the left mouse button is being clicked and held
        if (Input.GetMouseButton(0))
        {
            SetClickCursor();
        }
        else
        {
            SetDefaultCursor();
        }
    }

    private void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, cursorHotspotDefault, CursorMode.Auto);
    }

    private void SetClickCursor()
    {
        Cursor.SetCursor(clickCursor, cursorHotspotClick, CursorMode.Auto);
    }
}