using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : Minigame
{
    [Header("Canvas Components")]
    public TMP_Text timerText; //Time elapsed
    public TMP_Text shotDisplay; //Shots remaining text
    public GameObject gameWonPanel; //Win screen panel overlay
    public GameObject gameOverPanel; //Lose screen panel overlay

    [Header("Scoring image objects")]
    public GameObject[] successImages; // List of success images
    public GameObject[] failImages; // List of fail images

    [Header("Game Status")]
    public bool isGameOver; //Used to control endgame panels showing

    [Header("Counts")]
    public int shotsRemaining; //Remaining shots left
    private int successCount; //Shots into correct trigger
    private int failCount; //Shots into incorrect trigger
    private int maxCount; //Shots into trigger needed to win/lose

    [Header("Timer Components")]
    private float timer; //Onscreen timer value (displayed in text object)

    [Header("Other Components")]
    private SpawnerManager spawnManager; //Uses canSpawn from script to stop spawning at game end

    /// <summary>
    /// Sets initial values for game start/restart
    /// </summary>
    private void Start()
    {
        spawnManager = GameObject.Find("Managers/ObjectSpawner").GetComponent<SpawnerManager>(); //Get GameManager script from scene
        SetVariables();  //Set all initial values for game loading
        DisplayCounts(); //Set success/fail count text
    }
    
    /// <summary>
    /// Called in start/any restarting of game
    /// </summary>
    private void SetVariables()
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
    private void Update()
    {
        if (!isGameOver && gameStarted)
        {
            UpdateTimer();
            StartCoroutine(CheckGameStatus());
        }
    }

    /// <summary>
    /// Update the timer and format correctly, display onscreen in text object  <br />
    /// Colour changes to yellow towards 30s, red towards 1min
    /// </summary>
    private void UpdateTimer()
    {
        //Update current time value and seperate into mins, secs, millsec
        timer += Time.deltaTime;
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.Floor(timer % 60);
        float milliseconds = (timer * 1000) % 1000;
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

        // Interpolate color based on normalized time over 1min (60f = 60sec)
        float normalizedTime = Mathf.Clamp01(timer / 60f);
        timerText.color = Color.Lerp(Color.green, Color.yellow, normalizedTime * 2f); //Change to yellow fully at 30sec
        timerText.color = Color.Lerp(timerText.color, Color.red, (normalizedTime - 0.5f) * 2f); //Change to red fully at 1min
    }

    /// <summary>
    /// Accessed from Trigger 'RecycleCheck' script
    /// - Increases with correct placing of items in bins
    /// </summary>
    public void IncrementSuccessCount()
    {
        successCount++; //Increment counter
        DisplayCounts(); //Update onscreen display image/s 
        StartCoroutine(CheckGameStatus()); //See if game should end with current scores 
    }

    /// <summary>
    /// Accessed from Trigger 'RecycleCheck' script
    /// - Increases with incorrect placing of items in bins
    /// </summary>
    public void IncrementFailCount()
    {
        failCount++;
        DisplayCounts();
        StartCoroutine(CheckGameStatus());
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
    /// Set score images active for correct/incorrect placement of items
    /// </summary>
    private void DisplayCounts()
    {
        // Following code by Johnathan in Code Review
        // Clamp successCount and failCount to maxCount
        successCount = Mathf.Clamp(successCount, 0, maxCount);
        failCount = Mathf.Clamp(failCount, 0, maxCount);

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
    /// Resets all images for score back to false (not visible to user)
    /// </summary>
    private void ResetCountDisplay()
    {
        for (int i = 0; i < maxCount; i++)
        {
            successImages[i].SetActive(false);
            failImages[i].SetActive(false);
        }
    }

    /// <summary>
    /// Check if max scores have been reached/all shots used  <br /> 
    /// End game with win/lose panels accordingly
    /// </summary>
    /// <returns>Used to wait for last shot to land</returns>
    private IEnumerator CheckGameStatus()
    {
        if (successCount >= maxCount)
        {
            EndGameDisplay(gameWonPanel);
            success = true;
            InvokeGameOver();
            yield break;
        }

        if (failCount >= maxCount)
        {
            EndGameDisplay(gameOverPanel);
            success = false;
            InvokeGameOver();
            yield break;
        }

        if (shotsRemaining <= 0)
        {
            yield return new WaitForSeconds(3); //Wait 3 seconds before ending game
            
            if (!isGameOver) //Make sure game hasnt ended already after wait
            {
                EndGameDisplay(gameOverPanel);
                success = false;
                InvokeGameOver();
                yield break;
            }

        }
    }

    /// <summary>
    /// Set bools to end game/spawning<br />
    /// Shows panel for Game Won / Game Lost
    /// </summary>
    /// <param name="displayPanel">Panel displaying Game Won/Lost to player</param>
    private void EndGameDisplay(GameObject displayPanel)
    {
        isGameOver = true;
        spawnManager.canSpawn = false;
        displayPanel.SetActive(true);
    }

    /// <summary>
    /// Resets all required variables in Game/Spawner managers to start a new game   <br />
    ///  - Will be called from minigame launcher in final game. Temporary use of button OnClick() event to trigger reset at the moment
    /// </summary>
    public override void Reset()
    {
        //Game Manager Variables
        SetVariables(); //Set timer, success/fail counts and shots remaining back to game start defaults
        ResetCountDisplay(); //Set all checks/crosses back to inactive
        isGameOver = false; //Allows game to keep playing
        gameWonPanel.SetActive(false); //Hides any status panels shown at end of game
        gameOverPanel.SetActive(false);

        //Spawner Manager Variables
        Destroy(spawnManager.spawnedItem); //Stops spawning a new item ontop of item from previous game
        spawnManager.SetVariables(); //Set variables to start spawning new objects (canSpawn, firstSpawn, isSpawning)
        
        success = false; //Reset success bool to false
    }
}