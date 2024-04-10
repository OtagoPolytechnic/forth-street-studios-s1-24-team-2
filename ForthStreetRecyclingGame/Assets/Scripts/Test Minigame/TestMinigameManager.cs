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
public class TestMinigameManager : Minigame
{
    public int numberOfClicksRequired; // The number of times the player must click the button
    public int numberOfClicks; // The number of times the player has clicked the button
    public GameObject winText; // text object to activate if the player wins
    public GameObject clickText; // tmp text displaying the number of clicks

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
            // Fire the OnGameOver event
            OnGameOver.Invoke(success);
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
    public override void Reset()
    {
        numberOfClicks = 0;
        success = false;
        winText.SetActive(false);
        clickText.GetComponent<TextMeshProUGUI>().text = "0";
    }
}
