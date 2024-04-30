using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapItemToBin : MonoBehaviour
{
    private Vector3 wayPoint;
    public GameObject heldItem;
    public DragObject dragObject; //script reference to the drag object


    // Start is called before the first frame update
    void Start()
    {
        wayPoint = transform.GetChild(0).gameObject.transform.position; //gets the first child of the bin
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        //heldItem = dragObject.item; //gets the item from the drag object
        Debug.Log("Mouse Enter");
        heldItem.transform.position = wayPoint; //snaps the item to the bin
    }
}
