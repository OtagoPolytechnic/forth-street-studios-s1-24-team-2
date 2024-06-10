/*
 * File: CursorUpdate.cs
 * Author: Devon
 * Purpose: Updates the current cursor depending if the user is clicking or not
 */
using UnityEngine;

public class CursorUpdate : MonoBehaviour
{
    [SerializeField] private Texture2D defaultCursor; //Open hand cursor image
    [SerializeField] private Texture2D clickCursor; //Closed hand cursor image

    /// <summary>
    /// Set the default cursor initially
    /// </summary>
    private void Start()
    {
        SetCursor(defaultCursor);
    }

    /// <summary>
    /// Sets the current cursor depending on if mouse is clicked/held
    /// </summary>
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

    /// <summary>
    /// Updates the current cursor with new Texture and calculates click location for image
    /// </summary>
    /// <param name="cursor"></param>
    private void SetCursor(Texture2D cursor)
    {
        Vector2 cursorHotspot = new Vector2(cursor.width / 2, cursor.height / 2); //Centre of the cursor image registers the click position
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.Auto);
    }
}