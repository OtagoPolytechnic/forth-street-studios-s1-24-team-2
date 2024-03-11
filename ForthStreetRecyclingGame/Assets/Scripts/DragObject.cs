using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private const int ZLIMIT = -5; // Sends it to the z axis when clicked
    private GameObject item;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //tracks the mouse position
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) //when it collides, out hit stores information about the object
            {
                if (hit.rigidbody != null)
                {
                    item = hit.rigidbody.gameObject; //stores the object
                    item.transform.position = new Vector3(hit.point.x, hit.point.y, ZLIMIT); // specific object moves to the raycast target
                }
            }
        }   
    }
}
