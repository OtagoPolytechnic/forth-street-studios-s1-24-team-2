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
    private Outline outline;



    void Start()
    {
        cursorController = CursorController.Instance;
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        outline = GetComponent<Outline>();
        outline.enabled = false;
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

    private void OnMouseOver(){ outline.enabled = true; }
    private void OnMouseExit(){ outline.enabled = false; }

    public void ResetScale()
    {
        transform.localScale = originalScale;
    }
}