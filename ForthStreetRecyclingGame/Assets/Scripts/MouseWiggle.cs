using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseWiggle : MonoBehaviour
{
    private GameObject lid;
    [SerializeField]private Slider fill;

    // Start is called before the first frame update
    void Start()
    {
        lid = GameObject.Find("Lid");
        fill.value = 0;
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
            BarEmpty();
        }
    }

    private void LidMovement()
    {
        lid.transform.position = Vector3.MoveTowards(lid.transform.position, new Vector3(lid.transform.position.x, lid.transform.position.y + Time.deltaTime, lid.transform.position.z), Time.deltaTime * 0.01f);
    }

    private void BarMovement()
    {
        fill.value = Mathf.MoveTowards(fill.value, 1, Time.deltaTime * 0.08f);
    }

    private void BarEmpty()
    {
        fill.value = Mathf.MoveTowards(fill.value, 0, Time.deltaTime * 0.1f);
    }
}
