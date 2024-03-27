using UnityEngine;
using TMPro;
using UnityEditor.SearchService;

public class GameManager : MonoBehaviour
{
    //Variables set to public for testing/viewing of changes
    [Header("Canvas Components")]
    public TMP_Text timerText;
    public TMP_Text successText;
    public TMP_Text failText;
    public GameObject gameWonPanel;
    public GameObject gameOverPanel;

    [Header("Counts")]
    public int successCount;
    public int failCount;
    public int maxCount;
    public int shotsRemaining;

    [Header("Scoring image objects")]
    public GameObject[] successImages; // List of success images
    public GameObject[] failImages;    // List of fail images

    [Header("Game Status")]
    public bool isGameOver;
    public SpawnerManager spawnManager;

    [Header("Timer Components")]
    public float timer;
    private Color greenColor = Color.green;
    private Color yellowColor = Color.yellow;
    private Color redColor = Color.red;

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
        shotsRemaining = 20;
    }

    /// <summary>
    /// Update timer during gameplay
    /// </summary>
    void Update()
    {
        if (!isGameOver)
        {
            UpdateTimer();
            CheckGameStatus();
        }
    }

    /// <summary>
    /// Update the timer and format correctly, display onscreen in text object
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
        successCount++;
        DisplayCounts();
        CheckGameStatus();
    }

    /// <summary>
    /// Accessed from Trigger 'RecycleCheck' script
    /// - Increases with incorrect placing of items in bins
    /// </summary>
    public void IncrementFailCount()
    {
        failCount++;
        DisplayCounts();
        CheckGameStatus();
    }

    /// <summary>
    /// Set current text values showing correct/incorrect placement of items
    /// </summary>
    void DisplayCounts()
    {
        if (successCount > 3)
        {
            successCount = 3;
        }
        if (failCount > 3)
        {
            failCount = 3;
        }
        for (int i = 0; i < successCount; i++)
        {
            successImages[i].SetActive(true);
        }

        for (int i = 0; i < failCount; i++)
        {
            failImages[i].SetActive(true);
        }
    }

    public void DecreaseShotsRemaining()
    {
        shotsRemaining--;
    }

    public void CheckGameStatus()
    {
        if (shotsRemaining <= 0)
        {
            GameOver();
        }

        if (successCount >= maxCount)
        {
            GameWon();
        }

        if (failCount >= maxCount)
        {
            GameOver();
        }
    }

    void GameWon()
    {
        isGameOver = true;
        spawnManager.canSpawn = false;
        gameWonPanel.SetActive(true);
    }

    void GameOver()
    {
        isGameOver = true;
        spawnManager.canSpawn = false;
        gameOverPanel.SetActive(true);
    }
}