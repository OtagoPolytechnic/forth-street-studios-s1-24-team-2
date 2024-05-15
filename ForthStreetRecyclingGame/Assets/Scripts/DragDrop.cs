using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameObject Object; // The GameObject being dragged
    public GameObject ObjectDragPos; // The position where the Object should be dragged

    public float Dropdistance; // Distance within which the Object can be dropped

    public bool isLocked; // Flag to check if the Object is locked in place

    Vector2 ObjectInitPos; // Initial position of the Object

    void Start()
    {
        ObjectInitPos = Object.transform.position; // Save the initial position of the Object
    }

    public void DragObject() // Method to drag the Object
    {
        if (!isLocked) // If the Object is not locked, update its position to the mouse position
        { 
            Object.transform.position = Input.mousePosition;
        }
    }

    public void DropObject() // Method to drop the Object
    {
        // Calculate the distance between the Object's position and the drop position
        float Distance = Vector3.Distance(Object.transform.position, ObjectDragPos.transform.position);
        if (Distance < Dropdistance) // If the distance is within the drop distance, lock the Object in place
        {
            isLocked = true;
            Object.transform.position = ObjectDragPos.transform.position;
        }
        else // If the distance is greater than the drop distance, reset the Object to its initial position
        {
            Object.transform.position = ObjectInitPos;
        }
    }
}
