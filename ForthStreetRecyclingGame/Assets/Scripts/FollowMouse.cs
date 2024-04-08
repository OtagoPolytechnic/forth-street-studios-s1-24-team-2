using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private float smoothVelocity = 1;
    [SerializeField]private Camera cam;
    private Vector3 mousePos;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        transform.position = Vector3.Lerp(transform.position, new Vector3(mousePos.x, mousePos.y, transform.position.z), smoothVelocity * Time.deltaTime);
        //transform.LookAt(new Vector3(-mousePos.x, transform.rotation.y, -mousePos.z));
    }
}