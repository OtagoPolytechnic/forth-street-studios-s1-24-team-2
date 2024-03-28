/*
 * File: TestMinigameManager.cs
 * Purpose: Manage the test minigame
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using TMPro;
using UnityEngine;

/// <summary>
/// This class is used to manage the test minigame.
/// This is a simple game where the goal is to click a button a set number of times.
/// Upon doing this, the success condition is reached.
/// </summary>
public class TestMinigameManager : MonoBehaviour
{
    public RotateMonitor rotateMonitor;
    #region Singleton
    // Singleton pattern
    public static TestMinigameManager instance;

    void awake()
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

    // The number of times the player must click the button
    public int numberOfClicksRequired;

    // The number of times the player has clicked the button
    public int numberOfClicks;

    // The success condition
    public bool success;

    // text object to activate if the player wins
    public GameObject winText;

    // tmp text displaying the number of clicks
    public GameObject clickText;

    /// <summary>
    /// Initialise the game state
    /// </summary>
    void Start()
    {
        numberOfClicks = 0;
        success = false;
        winText.SetActive(false);
    }

    /// <summary>
    /// Check if the player has reached the success condition
    /// </summary>
    void Update()
    {
        if (!success && numberOfClicks >= numberOfClicksRequired)
        {
            success = true;
            winText.SetActive(true);
        }
    }

    /// <summary>
    /// Increment the number of clicks and update the text
    /// </summary>
    public void IncrementClicks()
    {
        numberOfClicks++;
        clickText.GetComponent<TextMeshProUGUI>().text = numberOfClicks.ToString();
    }

    /// <summary>
    /// Reset the game state
    /// </summary>
    public void ResetGame()
    {
        // log the call to reset the game
        Debug.Log("Resetting game");
        numberOfClicks = 0;
        success = false;
        winText.SetActive(false);
        clickText.GetComponent<TextMeshProUGUI>().text = "0";
        // log success
        Debug.Log("Success: " + success);
    }
}
