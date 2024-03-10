using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Renderer rend;
    private const int ZLIMIT = 4; //moves the 3d item to the back wall when clicked
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
                transform.position = new Vector3(hit.point.x, hit.point.y, ZLIMIT);
            }
        }
    }

    void OnMouseEnter()
    {
        rend.material.color = Color.blue;
    }

    void OnMouseExit()
    {
        rend.material.color = Color.white;
    }
}
