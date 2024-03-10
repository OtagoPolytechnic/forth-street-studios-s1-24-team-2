using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Renderer rend; //temporary
    private const int ZLIMIT = -5; // Sends it to the z axis when clicked

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
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
                transform.position = new Vector3(hit.point.x, hit.point.y, ZLIMIT); // new position
            }
        }
    }

    void OnMouseEnter()
    {
        rend.material.color = Color.blue; //visual cue
    }

    void OnMouseExit()
    {
        rend.material.color = Color.gray;
    }
}
