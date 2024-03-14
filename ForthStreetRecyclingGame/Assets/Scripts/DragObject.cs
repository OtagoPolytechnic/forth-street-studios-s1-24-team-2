using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private GameObject item;
    private float speed;
    private Vector3 targetPos;
    private bool pickedUp;

    void Start()
    {
        speed = 30f;
        targetPos = new Vector3(2f, 2.5f, -10f);
    }
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && pickedUp == false)
        {
            pickedUp = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //tracks the mouse position
            RaycastHit hit;
            Debug.Log("Picked up");
            if (Physics.Raycast(ray, out hit)) //when it collides, out hit stores information about the object
            {
                if (hit.rigidbody != null)
                {
                    item = hit.rigidbody.gameObject; //stores the object
                    StartCoroutine(MoveToPosition(item.transform, targetPos, speed)); //starts the coroutine to move object
                    item.GetComponent<Rigidbody>().isKinematic = true; //disables gravity
                }
            }
        }else if (pickedUp == true)
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

    private void Throw()
    {
        if (pickedUp == true && Input.GetMouseButtonDown(0))
        {
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 20f);
            pickedUp = false;
            Debug.Log("Thrown");

        }
    }
}
