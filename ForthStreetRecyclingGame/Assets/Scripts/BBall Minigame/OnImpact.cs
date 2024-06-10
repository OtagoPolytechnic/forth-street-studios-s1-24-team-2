// attach to a game object and play sfx on collision with another object

using UnityEngine;

public class OnImpact : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        // if game object tag is recycling, play the correct sound effect
        // get bball item type component from collided object
        BBallItemType itemType = gameObject.GetComponent<BBallItem>().itemType;

        if (itemType == BBallItemType.Can)
        {
            SFXManager.Instance.Play("CanHit2");
        }

        // if game object tag is rubbish, play the incorrect sound effect
        else if (itemType == BBallItemType.Apple)
        {
            SFXManager.Instance.Play("TrashHit");
        }
    }
}