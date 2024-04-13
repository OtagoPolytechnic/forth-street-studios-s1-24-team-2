using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FillMeter : MonoBehaviour
{
    [SerializeField] private Slider fill;
    [SerializeField] private GameObject meter;
    [SerializeField] private GameObject bottle;
    [SerializeField] private ParticleSystem confetti;
    private Rigidbody lidRb;

    //Const variables
    private const int MAX = 1; // slider scale is measured from 0 --> 1
    private const int LEFTSHAKE = 89; // rotates the meter slightly to shake it
    private const int RIGHTSHAKE = 91;
    private const int VERTICAL = 90; // used to stand the meter vertically
    private const int LIDPULL = 10; // used in add force to throw the lid into the air
    private const int ANIMATIONTIME = 2;
    private const float SHAKESPEED = 0.5f; // wait time between shakes
    private const float HALFMETER = 0.5f;
    private const float FILLSPEED = 0.3f; // how much the meter fills
    private const float EMPTYSPEED = 0.08f; // how much the meter empties

    // Start is called before the first frame update
    void Start()
    {
        fill.value = 0; //meter starts at 0
        confetti.Stop(); // stops the confetti from playing
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
        if (fill.value == MAX) //  if the meter is full
        {
            if (bottle.transform.GetChild(0).gameObject.GetComponent<Rigidbody>() == false) //checks first to see if there's already a rigidbody on the lid
            {
                StartCoroutine(LidRemoval());
                confetti.Play(); // plays the confetti
            }
        }
        else if (fill.value > HALFMETER) // checking the meter is half filled
        {
            StartCoroutine(ShakeMeter());
        }
        else //the meter stays horizontal the rest of the time
        {
            meter.transform.rotation = Quaternion.Euler(0, 0, VERTICAL);
        }
    }

    private void MouseMovement()
    {
        if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0) // mouse moving left and right
        {
            MeterUp();
        }
        if (Input.GetAxis("Mouse X") == 0) // mouse is still
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
        meter.transform.rotation = Quaternion.Euler(0, 0, RIGHTSHAKE);
        yield return new WaitForSeconds(SHAKESPEED);
        meter.transform.rotation = Quaternion.Euler(0, 0, LEFTSHAKE);
        yield return new WaitForSeconds(SHAKESPEED);
    }

    //Removes the lid from the bottle as if the player has fished it up!
    private IEnumerator LidRemoval()
    {
        lidRb = bottle.transform.GetChild(0).gameObject.AddComponent<Rigidbody>(); // adds rigidbody to the lid
        lidRb.useGravity = false; // disables gravity
        lidRb.AddForce(transform.up * LIDPULL); // adds rigidbody to the lid
        yield return new WaitForSeconds(ANIMATIONTIME);
        if (LidWiggleManager.instance != null)
        {
            LidWiggleManager.instance.InvokeGameOver();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reloads scene (temporary)
        }

    }
}