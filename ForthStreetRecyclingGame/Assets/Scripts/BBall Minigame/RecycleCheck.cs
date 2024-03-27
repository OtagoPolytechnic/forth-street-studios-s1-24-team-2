using UnityEngine;

/// <summary>
/// Checks the tag of object during collision against the trigger tag.
/// Marks as correct(green) / incorrect(red)
/// </summary>
public class RecycleCheck : MonoBehaviour
{
    public GameManager manager;

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
            switch (other.tag)// Check the tag of the collided object
            {
                case "Recycling":
                    manager.IncrementSuccessCount();
                    Destroy(other.gameObject);
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
            // Check the tag of the collided object
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
        else //Destroy any items that miss the main 2 trigger areas
        {
            Destroy(other.gameObject);
        }
    }
}