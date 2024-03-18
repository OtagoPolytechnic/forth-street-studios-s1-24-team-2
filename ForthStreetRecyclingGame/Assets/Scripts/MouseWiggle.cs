using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseWiggle : MonoBehaviour
{
    private GameObject lid;
    [SerializeField]private Image bar;

    // Start is called before the first frame update
    void Start()
    {
        lid = GameObject.Find("Lid");
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
    }

    private void MouseMovement()
    {
        if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0)
        {
            //Debug.Log("Mouse moved");
            //LidMovement();
            BarMovement();
        }
        if (Input.GetAxis("Mouse X") == 0)
        {
            //Debug.Log("Mouse stopped");
        }
    }

    private void LidMovement()
    {
        lid.transform.position = Vector3.MoveTowards(lid.transform.position, new Vector3(lid.transform.position.x, lid.transform.position.y + Time.deltaTime, lid.transform.position.z), Time.deltaTime * 0.01f);
    }

    private void BarMovement()
    {
        
    }
}
