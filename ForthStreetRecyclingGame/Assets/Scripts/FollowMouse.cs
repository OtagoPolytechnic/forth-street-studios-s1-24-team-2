using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private float smoothVelocity = 1;
    [SerializeField]private Camera cam;
    private Vector3 previousPosition;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cam.ScreenToWorldPoint
        (new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z)),
        smoothVelocity * Time.deltaTime);
    }
}
