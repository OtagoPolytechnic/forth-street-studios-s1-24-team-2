using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 targetPosition;
    private float smoothVelocity = 1;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint
        (new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)),
         smoothVelocity * Time.deltaTime);
    }
}
