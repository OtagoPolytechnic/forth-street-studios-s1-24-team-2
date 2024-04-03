using UnityEngine;

/// <summary>
/// Checks the tag of object during collision against the trigger tag.  <br />
/// Increments success / fail in the gamemanager accordingly (eg Recycling tag in recycle trigger is success)
/// </summary>
public class RecycleCheck : MonoBehaviour
{
    public GameManager manager; //Used to update current score counts

    /// <summary>
    /// Gets the GameManager from the scene to increment counts
    /// </summary>
    private void Start()
    {
        manager = GameObject.Find("Managers/GameManager").GetComponent<GameManager>(); //Get GameManager script from scene
    }

    /// <summary>
    /// Checks object collision for tag, incrementing the relevant counter, then destroys the gameobject
    /// </summary>
    /// <param name="other">Recycle/Rubbish gameobject being thrown into trigger area</param>
    void OnTriggerEnter(Collider other) 
    {
        //Check tag of the trigger area (Recycling/Rubbish)
        if (this.tag == "Recycling")
        {
            switch (other.tag) // Check the tag of the collided object
            {
                case "Recycling":
                    manager.IncrementSuccessCount(); //Increment onscreen score display
                    Destroy(other.gameObject); //Destroy GameObject to help performance 
                    break;

                case "Rubbish":
                    manager.IncrementFailCount();
                    Destroy(other.gameObject);
                    break;

                default:
                    break;
            }
        }
        else if (this.tag == "Rubbish")
        {
            switch (other.tag)
            {
                case "Recycling":
                    manager.IncrementFailCount();
                    Destroy(other.gameObject);
                    break;

                case "Rubbish":
                    manager.IncrementSuccessCount();
                    Destroy(other.gameObject);
                    break;

                default:
                    break;
            }
        }
        else if (this.tag == "Destroyer") //Destroy any items that miss the main 2 trigger areas and collide with third trigger area
        {
            Destroy(other.gameObject);
        }
    }
}