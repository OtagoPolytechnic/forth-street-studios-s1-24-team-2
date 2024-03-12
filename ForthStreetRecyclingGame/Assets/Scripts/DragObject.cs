using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private const int ZLIMIT = -5; // Sends it to the z axis when clicked
    private const int HALF = 2; // Used to divide the size of the object
    private const int BORDERWIDTH = 6; // Used to set the border width
    private GameObject item;

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

                    if (item.transform.position.y < item.transform.localScale.y / HALF) //if items try to move below half its size on the y axis, it will be stopped to prevent passing through the floor
                    {
                        item.transform.position = new Vector3(hit.point.x, item.transform.localScale.y / HALF, ZLIMIT); //versatile for different sized items
                    }
                }
            }
        }
    }
}
