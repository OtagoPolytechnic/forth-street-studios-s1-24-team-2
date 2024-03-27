using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //Variables set to public for testing/viewing of changes
    [Header("Canvas Components")]
    public TMP_Text timerText;      //Time elapsed
    public TMP_Text shotDisplay;    //Shots remaining text
    public GameObject gameWonPanel; //Win screen panel overlay
    public GameObject gameOverPanel;//Lose screen panel overlay

    [Header("Counts")]
    public int successCount; //Shots into correct trigger
    public int failCount;    //Shots into incorrect trigger
    public int maxCount;     //Shots into trigger needed to win/lose
    public int shotsRemaining; //Remaining shots left

    [Header("Scoring image objects")]
    public GameObject[] successImages; // List of success images
    public GameObject[] failImages;    // List of fail images

    [Header("Game Status")]
    public bool isGameOver;

    [Header("Timer Components")]
    public float timer;
    private Color greenColor = Color.green;
    private Color yellowColor = Color.yellow;
    private Color redColor = Color.red;

    [Header("Other Components")]
    public SpawnerManager spawnManager; //

    /// <summary>
    /// Sets initial values for game start/restart
    /// </summary>
    private void Start()
    {
        SetVariables();  //Set all initial values for game loading
        DisplayCounts(); //Set success/fail count text
        spawnManager = GameObject.Find("Managers/ObjectSpawner").GetComponent<SpawnerManager>(); //Get GameManager script from scene
    }
    
    /// <summary>
    /// Called in start/any restarting of game
    /// </summary>
    void SetVariables()
    {
        timer = 0f;
        successCount = 0;
        failCount = 0;
        maxCount = 3; //Game will end after 3 correct/incorrect objects
        timerText.text = "00:00:000";
        shotsRemaining = 10;
        shotDisplay.text = "Shots Remaining: " + shotsRemaining.ToString();
    }

    /// <summary>
    /// Update timer during gameplay and check if game needs to end
    /// </summary>
    void Update()
    {
        if (!isGameOver)
        {
            UpdateTimer();
            StartCoroutine(CheckGameStatusCoroutine());
        }
    }

    /// <summary>
    /// Update the timer and format correctly, display onscreen in text object  <br />
    /// Colour changes to yellow towards 30s, red towards 1min
    /// </summary>
    void UpdateTimer()
    {
        //Update current time value and seperate into mins, secs, millsec
        timer += Time.deltaTime;
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.Floor(timer % 60);
        float milliseconds = (timer * 1000) % 1000;
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

        // Interpolate color based on normalized time over 1min (60f = 60sec)
        float normalizedTime = Mathf.Clamp01(timer / 60f);
        timerText.color = Color.Lerp(greenColor, yellowColor, normalizedTime * 2f);            //Change to yellow fully around 30seconds
        timerText.color = Color.Lerp(timerText.color, redColor, (normalizedTime - 0.5f) * 2f); //Change to red fully at 1min
    }

    /// <summary>
    /// Accessed from Trigger 'RecycleCheck' script
    /// - Increases with correct placing of items in bins
    /// </summary>
    public void IncrementSuccessCount()
    {
        successCount++;                             //Increment counter
        DisplayCounts();                            //Update onscreen display image/s 
        StartCoroutine(CheckGameStatusCoroutine()); //See if game should end with current scores 
    }

    /// <summary>
    /// Accessed from Trigger 'RecycleCheck' script
    /// - Increases with incorrect placing of items in bins
    /// </summary>
    public void IncrementFailCount()
    {
        failCount++;
        DisplayCounts();
        StartCoroutine(CheckGameStatusCoroutine());
    }

    /// <summary>
    /// Set score images active for correct/incorrect placement of items
    /// </summary>
    void DisplayCounts()
    {
        //Keeps max score limited to maxCount to stop game breaking if objects hit trigger after game win/lose
        if (successCount > maxCount)
        {
            successCount = maxCount;
        }
        if (failCount > maxCount)
        {
            failCount = maxCount;
        }

        //Display images (check/cross) depending on the current score
        for (int i = 0; i < successCount; i++)
        {
            successImages[i].SetActive(true);
        }

        for (int i = 0; i < failCount; i++)
        {
            failImages[i].SetActive(true);
        }
    }

    /// <summary>
    /// Decrement the shots remaining counter and display as text to user   <br />
    /// Called in ThrowItem.OnMouseUp()
    /// </summary>
    public void DecreaseShotsRemaining()
    {
        shotsRemaining--;
        shotDisplay.text = "Shots Remaining: " + shotsRemaining.ToString();
    }

    /// <summary>
    /// Check if max scores have been reached/all shots used  <br /> 
    /// End game with win/lose panels accordingly
    /// </summary>
    /// <returns>Used to wait for last shot to land</returns>
    private IEnumerator CheckGameStatusCoroutine()
    {
        if (successCount >= maxCount)
        {
            EndGameDisplay(gameWonPanel);
        }

        if (failCount >= maxCount)
        {
            EndGameDisplay(gameOverPanel);
        }

        if (shotsRemaining <= 0)
        {
            yield return new WaitForSeconds(3); //Wait 3 seconds before ending game
            
            if (!isGameOver) //Make sure game hasnt ended already after wait
            {
                EndGameDisplay(gameOverPanel);
            }

        }
    }

    /// <summary>
    ///Set bools to end game/spawning<br />
    /// Shows panel for Game Won / Game Lost
    /// </summary>
    /// <param name="displayPanel">Panel displaying Game Won/Lost to player</param>
    void EndGameDisplay(GameObject displayPanel)
    {
        isGameOver = true;
        spawnManager.canSpawn = false;
        displayPanel.SetActive(true);
    }
}