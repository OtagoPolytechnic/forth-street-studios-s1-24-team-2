using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private GameObject item;
    private GameObject cam;
    private Vector3 targetPos;
    private float speed;
    private int force;
    private bool pickedUp;

    void Start()
    {
        speed = 30f;
        force = 300;
        cam = GameObject.Find("Main Camera");
        targetPos = new Vector3(cam.transform.position.x + 1.5f, cam.transform.position.y - 1f, cam.transform.position.z + 2.5f); //offset to the camera in a conventional gaming "held" position
    }
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && pickedUp == false)
        {
            pickedUp = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //tracks the mouse position
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) //when it collides, out hit stores information about the object
            {
                if (hit.rigidbody != null)
                {
                    item = hit.rigidbody.gameObject; //stores the object
                    StartCoroutine(MoveToPosition(item.transform, targetPos, speed)); //starts the coroutine to move object
                    item.GetComponent<Rigidbody>().isKinematic = true; //disables gravity
                }
            }
        }else if (pickedUp == true) //if there's already an item in the player's hand, throw it
        {
            Throw();
        }
    }

    //Moving the item over time
    IEnumerator MoveToPosition (Transform item, Vector3 target, float speed)
    {
        while (item.position != target)
        {
            item.position = Vector3.MoveTowards(item.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }

    //Used for swapping two items - chucks it back on the conveyer
    private void Throw()
    {
        if (Input.GetMouseButtonDown(0))
        {
            item.transform.position = new Vector3(0, item.transform.position.y, item.transform.position.z); //moves xpos back to 0 so it gets thrown into the centre
            item.GetComponent<Rigidbody>().isKinematic = false; //unfreezes item for gravity
            item.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * force); //adds force to the item
            pickedUp = false; //resets the bool so another item can be picked up
        }
    }
}
