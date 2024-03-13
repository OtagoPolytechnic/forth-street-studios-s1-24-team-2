using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private const int ZLIMIT = -5; // Sends it to the z axis when clicked
    private const int HALF = 2; // Used to divide the size of the object
    private GameObject item;
    private float speed = 30f;
    private Vector3 targetPos = new Vector3(2f, 2.5f, -10f);

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
                    StartCoroutine(MoveToPosition(item.transform, targetPos, speed)); //starts the coroutine to move object
                    Destroy(item.GetComponent<Rigidbody>()); //removes the rigidbody from the object
                }
            }
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
}
