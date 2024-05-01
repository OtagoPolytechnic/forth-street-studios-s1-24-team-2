using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapItemToBin : MonoBehaviour
{
    private Vector3 wayPoint;
    public GameObject heldItem;
    private DragObject dragObject; //script reference to the drag object


    // Start is called before the first frame update
    void Start()
    {
        dragObject = GameObject.Find("PickupObjects").GetComponent<DragObject>(); //finds the pickup object and gets the script
        wayPoint = transform.GetChild(0).gameObject.transform.position; //gets the first child of the bin
    }

    void OnMouseEnter()
    {
        heldItem = dragObject.item; //gets the item from the drag object
        heldItem.transform.position = wayPoint; //snaps the item to the bin
    }
}
