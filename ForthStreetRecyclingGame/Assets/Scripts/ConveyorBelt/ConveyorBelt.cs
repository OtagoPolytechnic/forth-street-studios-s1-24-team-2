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

        PickableObject pickupableObject = other.GetComponent<PickableObject>();
        bool shouldLieFlat = pickupableObject.ShouldLieFlat;

        Quaternion targetRotation = shouldLieFlat ? Quaternion.FromToRotation(other.transform.right, transform.up) : Quaternion.FromToRotation(other.transform.up, transform.up) * other.transform.rotation;

        float angleThreshold = 1.0f; // Set your desired threshold here

        // Calculate the angle difference between the current and target rotations
        float angleDifference = Quaternion.Angle(other.transform.rotation, targetRotation * other.transform.rotation);

        // Only perform the slerp operation if the angle difference is above the threshold
        if (angleDifference > angleThreshold)
        {
            other.transform.rotation =
                shouldLieFlat ?
                    Quaternion.Slerp(other.transform.rotation, targetRotation * other.transform.rotation, Time.deltaTime * rotationModifier) :
                    Quaternion.Slerp(other.transform.rotation, targetRotation, Time.deltaTime * rotationModifier);
        }


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