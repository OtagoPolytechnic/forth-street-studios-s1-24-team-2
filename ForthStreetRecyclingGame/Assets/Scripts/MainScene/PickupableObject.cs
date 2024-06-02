/*
 * File: PickupableObject.cs
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// Controls the pickup effect for the pickupable objects in the main scene
/// </summary>
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
        // Prevent object from falling while held
        rb.isKinematic = true;
        cursorController.PickUpObject(gameObject);
    }

    void OnMouseUp()
    {
        // Allow object to fall when dropped
        rb.isKinematic = false;
        cursorController.DropObject();
    }
}