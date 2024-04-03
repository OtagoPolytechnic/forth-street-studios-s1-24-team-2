using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FillMeter : MonoBehaviour
{
    [SerializeField]private Slider fill;
    [SerializeField]private GameObject meter;
    [SerializeField]private GameObject bottle;
    private Rigidbody lidRb;

    //Const variables
    private const int MAX = 1;
    private const int LEFTSHAKE = 89;
    private const int RIGHTSHAKE = 91;
    private const int HORIZONTAL = 90;
    private const int LIDPULL = 10;
    private const int ANIMATIONTIME = 2;
    private const float SHAKESPEED = 0.5f;
    private const float FILLSPEED = 0.3f;
    private const float EMPTYSPEED = 0.08f;

    // Start is called before the first frame update
    void Start()
    {
        fill.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
        if (fill.value == MAX) // if the meter is full
        {
            StartCoroutine(LidRemoval());
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
        fill.value = Mathf.MoveTowards(fill.value, 1, Time.deltaTime * FILLSPEED); // fills the meter
    }

    private void MeterDown()
    {
        fill.value = Mathf.MoveTowards(fill.value, 0, Time.deltaTime * EMPTYSPEED); // empties the meter
    }

    //Shake the meter when it's getting close to the top
    private IEnumerator ShakeMeter()
    {
        if (fill.value > SHAKESPEED)
        {
            meter.transform.rotation = Quaternion.Euler(0, 0, RIGHTSHAKE);
            yield return new WaitForSeconds(SHAKESPEED);
            meter.transform.rotation = Quaternion.Euler(0, 0, LEFTSHAKE);
            yield return new WaitForSeconds(SHAKESPEED);
        }
        else
        {
            meter.transform.rotation = Quaternion.Euler(0, 0, HORIZONTAL);
        }
    }

    //Removes the lid from the bottle as if the player has fished it up!
    private IEnumerator LidRemoval()
    {
        if (bottle.transform.GetChild(0).gameObject.GetComponent<Rigidbody>() == false)
        {
            lidRb = bottle.transform.GetChild(0).gameObject.AddComponent<Rigidbody>(); // adds rigidbody to the lid
            lidRb.useGravity = false; // disables gravity
            lidRb.AddForce(transform.up * LIDPULL); // adds rigidbody to the lid
        }
        yield return new WaitForSeconds(ANIMATIONTIME);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reloads scene (temporary)

    }
}
