// attach to a game object and play sfx on collision with another object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnImpact : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("This object: " + gameObject.name + " collided with " + collision.gameObject.name);
        // if game object tag is recycling, play the correct sound effect
        // get bball item type component from collided object
        BBallItemType itemType = gameObject.GetComponent<BBallItem>().itemType;

        Debug.Log("Item type: " + itemType);

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