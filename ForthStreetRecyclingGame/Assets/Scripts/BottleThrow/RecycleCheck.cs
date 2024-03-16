using UnityEngine;

/// <summary>
/// Checks the tag of object during collision against the trigger tag.
/// Marks as correct(green) / incorrect(red)
/// </summary>
public class RecycleCheck : MonoBehaviour
{
    Renderer rend;

    // When thrown object enters the trigger, sets the colour of the object depending on the trigger tag
    /*
    *  Colour change of the objects is temporary for now and will update with more functionality later.
    *  Useful for prototyping stage
    */
    void OnTriggerEnter(Collider other) 
    {
        rend = other.GetComponent<Renderer>();
        Color colourToSet = Color.grey; //Set to temporary colour before check

        //Check tag of the trigger area (Recycling/Rubbish)
        if (this.tag == "Recycling")
        {
            switch (other.tag)// Check the tag of the collided object
            {
                case "Recycling":
                    colourToSet = Color.green;
                    break;
                case "Rubbish":
                    colourToSet = Color.red;
                    break;
                default:
                    break;
            }
        }
        else if (this.tag == "Rubbish")
        {
            // Check the tag of the collided object
            switch (other.tag)
            {
                case "Recycling":
                    colourToSet = Color.red;
                    break;
                case "Rubbish":
                    colourToSet = Color.green;
                    break;
                default:
                    break;
            }
        }
        else //Destroy any items that miss the main 2 trigger areas
        {
            Destroy(other.gameObject);
        }

        rend.material.color = colourToSet;
    }
}