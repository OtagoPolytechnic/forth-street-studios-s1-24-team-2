using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapItemToBin : MonoBehaviour
{
    private Vector3 wayPoint;
    public GameObject heldItem;
    private DragObject dragObject; //script reference to the drag object
    private string binType;

    // Start is called before the first frame update
    void Start()
    {
        dragObject = GameObject.Find("PickupObjects").GetComponent<DragObject>(); //finds the pickup object and gets the script
        wayPoint = transform.GetChild(0).gameObject.transform.position; //gets the first child of the bin
    }

    void Update()
    {
        //GetMouseButtonDown doesn't work in a method that is a mouse event itself, so it's moved to Update and type is stored
        if (Input.GetMouseButtonDown(0) && binType != null) // if the player clicks a bin
        {
            BinCheck(); // checks the type
        }
    }

    void OnMouseEnter()
    {
        if (dragObject.item == null) return; //if the item is null, return (do nothing)
        heldItem = dragObject.item; //gets the item from the drag object
        heldItem.transform.position = wayPoint; //snaps the item to the bin
        binType = gameObject.name;
    }

    void OnMouseExit()
    {
        binType = null;
    }

    private void BinCheck()
    {
        switch (binType)
        {
            case "Recycling":
                Debug.Log("Recycling");
                binType = "Recycling";
                // Minigame
                return;
            case "Rubbish":
                Debug.Log("Rubbish");
                binType = "Rubbish";
                Destroy(heldItem);
                return;
            case "Glass":
                Debug.Log("Glass");
                binType = "Glass";
                // Minigame
                return;
        }
    }
}
