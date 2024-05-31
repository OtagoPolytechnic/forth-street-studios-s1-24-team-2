using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public CursorController cursorController;

    // on start assign the static instance of the cursor controller
    void Start()
    {
        cursorController = CursorController.instance;
    }   
    
    void OnMouseDown()
    {
        cursorController.PickUpObject(gameObject);
    }

    void OnMouseUp()
    {
        cursorController.DropObject();
    }
}