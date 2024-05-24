using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LidWiggleManager : Minigame
{
    [SerializeField] private Slider fill;
    [SerializeField] private GameObject meter;
    [SerializeField] private GameObject lid;
    [SerializeField] private GameObject bottle;
    [SerializeField] private float LIDPULL = 1; // used in add force to throw the lid into the air
    [SerializeField] private ParticleSystem confetti;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private MouseAnimation mouseAnimation;

    private Vector3 initBottlePos;
    private Vector3 initLidPos;
    private float timeLeft;
    private float timeLimit = 10.0f;

    //Const variables
    private const int MAX = 1; // slider scale is measured from 0 --> 1
    private const int LEFTSHAKE = 89; // rotates the meter slightly to shake it
    private const int RIGHTSHAKE = 91;
    private const int VERTICAL = 90; // used to stand the meter vertically
    private const int ANIMATIONTIME = 1;
    private const float SHAKESPEED = 0.5f; // wait time between shakes
    private const float HALFMETER = 0.5f;
    private const float FILLSPEED = 0.3f; // how much the meter fills
    private const float EMPTYSPEED = 0.08f; // how much the meter empties

    private Coroutine timerCoroutine;

    #region Singleton
    // Singleton pattern
    public static LidWiggleManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        initBottlePos = bottle.transform.position;
        initLidPos = lid.transform.position;
        timeLeft = timeLimit; // sets the time limit
        timerText.text = timeLeft.ToString("F2"); //Shows one decimal point
        fill.value = 0; //meter starts at 0
        confetti.Stop(); // stops the confetti from playing
    }

    void Update()
    {
        if (!gameStarted || success) { return; }
        MouseMovement();
        if (fill.value == MAX) //  if the meter is full
        {
            SFXManager.Instance.Play("BottleOpen");
            success = true;
            InvokeGameOver();
            StartCoroutine(LidRemoval());
            confetti.Play(); // plays the confetti
            
        }
        else if (fill.value > HALFMETER) // checking the meter is half filled
        {
            StartCoroutine(ShakeMeter());
        }
        else //the meter stays horizontal the rest of the time
        {
            meter.transform.rotation = Quaternion.Euler(0, 0, VERTICAL);
        }

        if (timeLeft <= 0 && gameStarted) // if time runs out
        {
            success = false;
            gameStarted = false;
            InvokeGameOver();
        }
    }

    public override void MinigameBegin()
    {   
        base.MinigameBegin();
        confetti.Stop();
        StartTimer();
        mouseAnimation.PlayTutorialAnimation();
        lid.transform.SetParent(bottle.transform);
    }

    private void MouseMovement()
    {
        if (fill.value == MAX) { return; }
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

    // Coroutine to remove the lid
    IEnumerator LidRemoval()
    {       
        lid.transform.SetParent(bottle.transform.parent);
        float elapsedTime = 0;
        float duration = ANIMATIONTIME;
        Vector3 initialPosition = lid.transform.position;
        Vector3 finalPosition = lid.transform.position + lid.transform.up * LIDPULL;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // Normalized time between 0 and 1
            lid.transform.position = Vector3.Lerp(initialPosition, finalPosition, 1 - (1 - t) * (1 - t)); // Start fast, slow down towards the end

            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }


    }


    public IEnumerator StartTimerCoroutine()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(0.1f); // goes down every 0.1 seconds
            timeLeft -= 0.1f;
            timerText.text = timeLeft.ToString("F1"); //Shows one decimal point

            if (timeLeft <= 5.1)
            {
                timerText.color = Color.red; // warning sign
            }
        }
    }

    void StartTimer()
    {
        timerCoroutine = StartCoroutine(StartTimerCoroutine());
    }

    void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    private void ResetTimer()
    {
        StopTimer();
        timeLeft = timeLimit;
        timerText.color = Color.white;
        timerText.text = timeLeft.ToString("F2");
    }

    private void ResetBottle()
    {
        // get follow mouse component off bottle
        FollowMouse followMouse = bottle.GetComponent<FollowMouse>();
        followMouse.Reset();
        bottle.transform.position = initBottlePos;
        lid.transform.position = initLidPos;
    }

    public override void Reset()
    {
        ResetBottle();
        gameStarted = false;
        success = false;
        confetti.Stop();
        ResetTimer();
        fill.value = 0;
    }

    [ContextMenu("Test")]
    public void Test()
    {
        Debug.Log("Test");
        SFXManager.Instance.Play("BottleOpen");
    }
}