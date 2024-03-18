using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWiggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
    }

    private void MouseMovement()
    {
        if (Input.GetAxis("Mouse X") < 0)
        {
            Debug.Log("Mouse moved left");
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            Debug.Log("Mouse moved right");
        }
        if (Input.GetAxis("Mouse X") == 0)
        {
            Debug.Log("Mouse stopped");
        }
    }
}
