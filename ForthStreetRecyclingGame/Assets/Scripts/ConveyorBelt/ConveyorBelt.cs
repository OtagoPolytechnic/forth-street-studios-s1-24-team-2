/* 
 * File: ConveyorBelt.cs
 * Purpose: Checks box collider trigger area for collided objects (Recycle/Rubbish)
 *          Disables gravity and moves items down conveyor at correct angle
 *          Re-enables gravity once item leaves conveyor
 * Author: Devon
 * Contributions: Help from ChatGPT for getting objects to rotate according to conveyor belt rotation -- end of OnTriggerStay() FromToRotation and Slerp
 * 
 */

using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] public float speed = 1f; // Speed of the conveyor belt
    private float rotationModifier = 10f; //Speed up item rotation to match conveyor angle

    /// <summary>
    /// Active while gameobject is in trigger area                                          <br />
    /// Disables gravity on object rb, moves in direction of conveyor at a constant speed   <br />
    /// Items get quickly rotated to match conveyor angle
    /// </summary>
    /// <param name="other">Gameplay object (Recycle/rubbish)</param>
    private void OnTriggerStay(Collider other)
    {
        //Get the rb and disable gravity while on conveyor belt
        Rigidbody rb = other.GetComponent<Rigidbody>(); 
        rb.useGravity = false; 

        // Calculate direction and move the object along the conveyor belt
        Vector3 direction = -transform.forward; 
        rb.velocity = direction * speed;

        // Rotate the object to match the conveyor's rotation (CHATGPT)
        Quaternion rotation = Quaternion.FromToRotation(other.transform.up, transform.up) * other.transform.rotation;
        other.transform.rotation = Quaternion.Slerp(other.transform.rotation, rotation, Time.deltaTime * rotationModifier);
    }

    /// <summary>
    /// Enables gravity again when object.rb has left the conveyor belt
    /// </summary>
    /// <param name="other">Gameplay object (Recycle/rubbish)</param>
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.useGravity = true;
    }
}