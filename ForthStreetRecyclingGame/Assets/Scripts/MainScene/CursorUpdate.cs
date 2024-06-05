using UnityEngine;

public class CursorUpdate : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D clickCursor;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SetCursor(clickCursor);
        }
        else
        {
            SetCursor(defaultCursor);
        }
    }

    private void SetCursor(Texture2D cursor)
    {
        Vector2 hotspot = new Vector2(cursor.width / 2, cursor.height / 2); //Where the click is registered on the texture [ Centred ]
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }
}