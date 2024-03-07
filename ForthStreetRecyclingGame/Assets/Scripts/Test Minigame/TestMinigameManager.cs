using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This class is used to manage the test minigame.
/// This is a simple game where the goal is to click a button a set number of times.
/// Upon doing this, the success condition is reached.
/// </summary>
public class TestMinigameManager : MonoBehaviour
{
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
    private bool success;

    // text object to activate if the player wins
    public GameObject winText;

    // tmp text displaying the number of clicks
    public GameObject clickText;

    // Start is called before the first frame update
    void Start()
    {
        numberOfClicks = 0;
        success = false;
        winText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfClicks >= numberOfClicksRequired)
        {
            success = true;
            winText.SetActive(true);
        }


    }

    // function to increment the number of clicks
    public void IncrementClicks()
    {
        numberOfClicks++;
        clickText.GetComponent<TextMeshProUGUI>().text = numberOfClicks.ToString();
    }

    // function to reset game state
    public void ResetGame()
    {
        numberOfClicks = 0;
        success = false;
        winText.SetActive(false);
        // get tmp, clicktext shows 0
        clickText.GetComponent<TextMeshProUGUI>().text = "0";

    }
}
