using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public CursorController cursorController;
    private Rigidbody rb;

    void Start()
    {
        cursorController = CursorController.Instance;
        rb = GetComponent<Rigidbody>();
    }   
    
    void OnMouseDown()
    {
        rb.isKinematic = true;
        cursorController.PickUpObject(gameObject);
    }

    void OnMouseUp()
    {
        rb.isKinematic = false;
        cursorController.DropObject();
    }
}