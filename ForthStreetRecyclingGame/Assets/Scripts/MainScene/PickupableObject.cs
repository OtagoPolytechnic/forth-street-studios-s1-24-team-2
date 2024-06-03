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
    [field: SerializeField] public bool ShouldLieFlat { get; private set; }

    private CursorController cursorController;
    private Rigidbody rb;
    private Vector3 originalScale;



    void Start()
    {
        cursorController = CursorController.Instance;
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
    }   

    void OnMouseDown()
    {
        // Prevent object from falling while held
        rb.isKinematic = true;
        cursorController.PickUpObject(gameObject);
        // increase object size
        transform.localScale = originalScale * cursorController.ScaleIncrease;
    }

    void OnMouseUp()
    {
        // Allow object to fall when dropped
        rb.isKinematic = false;
        cursorController.DropObject();
    }

    public void ResetScale()
    {
        transform.localScale = originalScale;
    }
}