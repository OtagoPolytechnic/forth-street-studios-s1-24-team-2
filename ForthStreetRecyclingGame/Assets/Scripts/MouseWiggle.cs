using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseWiggle : MonoBehaviour
{
    private GameObject lid;
    [SerializeField]private Slider fill;
    [SerializeField]private GameObject meter;
    private Vector3 lidStartPos;

    // Start is called before the first frame update
    void Start()
    {
        lid = GameObject.Find("Lid");
        lidStartPos = lid.transform.position;
        fill.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
        if (fill.value == 1) // if the meter is full
        {
            Debug.Log("You win!"); // you win
        }
        StartCoroutine(ShakeMeter());
    }

    private void MouseMovement()
    {
        if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0)
        {
            MeterUp();
        }
        if (Input.GetAxis("Mouse X") == 0)
        {
            MeterDown();
        }
    }

    private void MeterUp()
    {
        fill.value = Mathf.MoveTowards(fill.value, 1, Time.deltaTime * 0.08f); // fills the meter
        lid.transform.position = Vector3.MoveTowards(lid.transform.position,
        new Vector3(lid.transform.position.x, lid.transform.localPosition.y + 5, lid.transform.position.z), Time.deltaTime * 0.01f); // moves the lid up
        lid.transform.Rotate(new Vector3(0, 20f, 0) * Time.deltaTime); // rotates the lid
    }

    private void MeterDown()
    {
        if (lid.transform.position.y >= lidStartPos.y)
        {
            fill.value = Mathf.MoveTowards(fill.value, 0, Time.deltaTime * 0.1f); // empties the meter
            lid.transform.position = Vector3.MoveTowards(lid.transform.position,
            new Vector3(lid.transform.position.x, -lid.transform.localPosition.y + 5, lid.transform.position.z), Time.deltaTime * 0.01f); // moves the lid down
            lid.transform.Rotate(new Vector3(0, 20f, 0) * Time.deltaTime); // rotates the lid
        }
        else
        {
            lid.transform.position = lidStartPos; // so the lid doesn't keep going down
            lid.transform.Rotate(new Vector3(0, 0, 0)); // so the lid doesn't keep rotating
        }
    }

    //Shake the meter when it's getting close to the top
    private IEnumerator ShakeMeter()
    {
        if (fill.value > 0.5)
        {
            meter.transform.rotation = Quaternion.Euler(0, 0, 91);
            yield return new WaitForSeconds(0.5f);
            meter.transform.rotation = Quaternion.Euler(0, 0, 89);
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            meter.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
