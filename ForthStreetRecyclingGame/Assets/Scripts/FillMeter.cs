using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FillMeter : MonoBehaviour
{
    [SerializeField]private Slider fill;
    [SerializeField]private GameObject meter;
    [SerializeField]private GameObject bottle; //for the bottle object to flip when the mouse moves

    // Start is called before the first frame update
    void Start()
    {
        fill.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
        if (fill.value == 1) // if the meter is full
        {
            Debug.Log("You win!"); // you win
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reloads scene (temporary)
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
        fill.value = Mathf.MoveTowards(fill.value, 1, Time.deltaTime * 0.3f); // fills the meter
    }

    private void MeterDown()
    {
        fill.value = Mathf.MoveTowards(fill.value, 0, Time.deltaTime * 0.08f); // empties the meter
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
