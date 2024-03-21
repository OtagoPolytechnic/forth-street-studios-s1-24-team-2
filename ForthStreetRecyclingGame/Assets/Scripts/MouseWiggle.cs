using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseWiggle : MonoBehaviour
{
    private GameObject lid;
    [SerializeField]private Slider fill;
    [SerializeField]private GameObject meter;

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
        if (fill.value == 1)
        {
            Debug.Log("You win!");
        }
    }

    private void MouseMovement()
    {
        if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0)
        {
            MeterUp();
            StartCoroutine(ShakeMeter());
        }
        if (Input.GetAxis("Mouse X") == 0)
        {
            MeterDown();
            StopCoroutine(ShakeMeter());
        }
    }

    private void MeterUp()
    {
        fill.value = Mathf.MoveTowards(fill.value, 1, Time.deltaTime * 0.1f);
        lid.transform.position = Vector3.MoveTowards(lid.transform.position, new Vector3(lid.transform.position.x, lid.transform.position.y + Time.deltaTime, lid.transform.position.z), Time.deltaTime * 0.01f);
    }

    private void MeterDown()
    {
        fill.value = Mathf.MoveTowards(fill.value, 0, Time.deltaTime * 0.1f);
        lid.transform.position = Vector3.MoveTowards(lid.transform.position, new Vector3(lid.transform.position.x, -lid.transform.position.y + Time.deltaTime, lid.transform.position.z), Time.deltaTime * 0.01f);
    }

    private IEnumerator ShakeMeter()
    {
        if (fill.value > 5)
        {
            meter.transform.rotation = Quaternion.Euler(0, 0, 91);
            yield return new WaitForSeconds(0.5f);
            meter.transform.rotation = Quaternion.Euler(0, 0, 89);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
