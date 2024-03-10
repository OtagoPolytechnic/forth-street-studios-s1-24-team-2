using UnityEngine;

/// <summary>
/// Checks the tag of object during collision and marks as correct(green)/incorrect(red)
/// </summary>
/*
*  Colour change of the objects is temporary for now and will update with more functionality later.
*  Useful for prototyping stage
*/
public class RecycleCheck : MonoBehaviour
{
    Renderer rend;

    void OnTriggerEnter(Collider other) // When thrown object enters the trigger
    {
        rend = other.GetComponent<Renderer>();

        // Check the tag of the collided object
        switch (other.tag)
        {
            case "Recycling":
                rend.material.color = Color.green;
                break;
            case "Rubbish":
                rend.material.color = Color.red;
                break;
            default:
                break;
        }
    }
}