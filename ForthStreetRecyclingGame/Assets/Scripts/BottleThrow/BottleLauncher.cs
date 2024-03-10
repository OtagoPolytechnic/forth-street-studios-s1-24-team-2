using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleLauncher : MonoBehaviour
{
    private GameObject bottle;
    float 
        startTime,
        endTime,
        swipeDistance,
        swipeTime;

    private Vector2 startPos;
    private Vector2 endPos;

    public float MinSwipeDist = 0;
    private float bottleVelocity = 0;
    private float bottleSpeed = 0;
    public float maxBottleSpeed = 350;
    private Vector3 angle;

    private bool
        thrown,
        holding;

    private Vector3 newPosition;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        SetupBottle();
    }

    void SetupBottle()
    {
        GameObject _bottle = GameObject.FindGameObjectWithTag("Player");
        bottle = _bottle;
        rb = bottle.GetComponent<Rigidbody>();
        ResetBottle();
    }

    void ResetBottle()
    {
        angle = Vector3.zero;
        endPos = Vector2.zero;
        startPos = Vector2.zero;
        bottleSpeed = 0;
        startTime = 0;
        endTime = 0;
        swipeDistance = 0;
        swipeTime = 0;
        thrown = holding = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        bottle.transform.position = transform.position;
    }

    void PickupBottle()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane * 5f;
        newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        bottle.transform.localPosition = Vector3.Lerp(bottle.transform.localPosition, newPosition, 80f * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (holding)
        {
            PickupBottle();
        }

        if (thrown)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;

            if (Physics.Raycast(ray, out _hit, 100f))
            {
                if(_hit.transform == bottle.transform)
                {
                    startTime =Time.time;
                    startPos = Input.mousePosition;
                    holding = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            endPos = Input.mousePosition;
            swipeDistance = (endPos - startPos).magnitude;
            swipeTime = endTime - startTime;

            if (swipeTime < 0.5f && swipeDistance > 30f)
            {
                //Throw Bottle
                CalculateSpeed();
                CalculateAngle();
                rb.AddForce(new Vector3((angle.x * bottleSpeed), (angle.y * bottleSpeed / 3), (angle.z * bottleSpeed) * 2));
                rb.useGravity = true;
                holding = false;
                thrown = true;
                Invoke("ResetBottle", 45f);
            }
            else
            {
                ResetBottle();
            }
        }
    }

    void CalculateSpeed()
    {
        if (swipeTime > 0)
        {
            bottleVelocity = swipeDistance / (swipeDistance - swipeTime);
            bottleSpeed = bottleVelocity * 40;
        }

        if (bottleSpeed <= maxBottleSpeed)
        {
            bottleSpeed = maxBottleSpeed;
        }
        swipeTime = 0;
    }

    private void CalculateAngle()
    {
        angle = Camera.main.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y + 50f, (Camera.main.nearClipPlane + 5)));

    }
}
