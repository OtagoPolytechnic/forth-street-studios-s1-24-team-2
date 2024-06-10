using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoundary : MonoBehaviour
{
    private float boundsX;
    // Start is called before the first frame update
    void Start()
    {
        boundsX = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > boundsX)
        {
            transform.position = new Vector3(boundsX, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -boundsX)
        {
            transform.position = new Vector3(-boundsX, transform.position.y, transform.position.z);
        }
    }
}
