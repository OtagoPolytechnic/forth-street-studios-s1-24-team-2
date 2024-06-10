/*
 * File: MainGameManager.cs
 * Purpose: Manage the game state
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// Game manager for tracking the state of the game.
/// </summary>
public class MainGameManager : MonoBehaviour
{
    [Header("Optimisation")]
    [SerializeField] private int targetFrameRate = 60;

    [Header("Gameplay")]
    [SerializeField] private int MinigameReputationChange = 10; // The amount of reputation the player gains or loses from a minigame
    [SerializeField] private int BinReputationChange = 5; // The amount of reputation the player gains or loses from putting an item in a bin / falling off the conveyor belt
    [SerializeField] private MinigameLauncher minigameLauncher; // Reference to the MinigameLauncher script
    [SerializeField] private float minigameDelay = 1; // The delay before launching a minigame after placing waste in the correct bin

    [Header("UI")]
    [SerializeField] private Slider reputationSlider; // The slider that displays the player's reputation
    [SerializeField] private TMP_Text gameEndText;
    [SerializeField, TextArea] private string gameLostBlurb; // The blurb that is displayed when the game is over
    [SerializeField, TextArea] private string gameWonBlurb; // The blurb that is displayed when the game is over
    [SerializeField] private GameObject gameOverPanel; // The panel that is displayed when the game is over
    [SerializeField] private float sliderAnimationDuration = 0.3f; // Duration of the animation in seconds
    [SerializeField] private GameObject inputBlocker;

    [Header("Managers")]
    [SerializeField] private CursorController cursorController;

    [HideInInspector] public UnityEvent<bool> mainGameOver; // Event that is fired when the main game is over
    [HideInInspector] public UnityEvent reset; // Event that is fired when the main game is over

    public int Reputation { get; private set; } = 50; // The player's reputation   
    
    /// <summary>
    /// Setter and getter for the target frame rate.
    /// </summary>
    public int TargetFrameRate
    {
        get { return targetFrameRate; }
        set
        {
            targetFrameRate = value;
            // apply the new target frame rate
            Application.targetFrameRate = targetFrameRate;
        }
    }

    /// <summary>
    /// Workaround for calling setter logic in the inspector.
    /// </summary>
    void OnValidate()
    {
        TargetFrameRate = targetFrameRate;
    }

    #region Singleton
    public static MainGameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void Start()
    {
        // Set the target frame rate
        Application.targetFrameRate = targetFrameRate;
        reputationSlider.value = (float)Reputation / 100;
        // consume minigameover event from minigamelauncher
        minigameLauncher.minigameOver.AddListener(HandleMinigameOver);
        BlockInput(false);
    }

    /// <summary>
    /// Handle the placement of waste in the correct or incorrect bin.
    /// </summary>
    /// <param name="correct">Whether the waste was placed in the correct bin.</param>
    public void HandleWastePlacement(bool correct)
    {
        // If correct, increase reputation and launch a minigame after a short delay
        if (correct)
        {
            Reputation += BinReputationChange;
            StartCoroutine(CorrectBinRoutine());
        }
        else
        {
            BlockInput(false);
            SFXManager.Instance.Play("Miss");
            Reputation -= BinReputationChange;
        }

        UpdateUI();
    }

    /// <summary>
    /// Update the UI to reflect the current reputation.
    /// </summary>
    private void UpdateUI()
    {
        // Start the coroutine to animate the slider
        StartCoroutine(AnimateSliderChange(reputationSlider, (float)Reputation / 100));
    }

    /// <summary>
    /// Animate the change in the slider value.
    /// </summary>
    /// <param name="slider">The slider to animate.</param>
    /// <param name="newValue">The new value of the slider.</param>
    private IEnumerator AnimateSliderChange(Slider slider, float newValue)
    {
        float elapsedTime = 0;
        float animationDuration = sliderAnimationDuration; // Duration of the animation in seconds
        float startingValue = slider.value;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float newSliderValue = Mathf.Lerp(startingValue, newValue, elapsedTime / animationDuration);
            slider.value = newSliderValue;
            yield return null;
        }

        // Ensure the slider value is exactly the new value at the end of the animation
        slider.value = newValue;

        if (Reputation <= 0 || Reputation >= 100)
        {
            HandleGameOver(Reputation >= 100);
        }
    }

    /// <summary>
    /// Handle the end of the minigame.
    /// </summary>
    /// <param name="win">Whether the player won the minigame.</param>
    private void HandleMinigameOver(bool win)
    {
        if (win)
        {
            SFXManager.Instance.Play("RepUp");
            Reputation += MinigameReputationChange;
        }
        else
        {
            SFXManager.Instance.Play("RepDown");
            Reputation -= MinigameReputationChange;
        }

        UpdateUI();
    }

    /// <summary>
    /// Coroutine to handle the player placing waste in the correct bin.
    /// </summary>
    private IEnumerator CorrectBinRoutine() 
    {
        SFXManager.Instance.Play("Correct");
        yield return new WaitForSeconds(minigameDelay);
        if (Reputation > 0 && Reputation < 100)
        {
            minigameLauncher.LaunchRandomMinigame();
        }
    }


    /// <summary>
    /// Handle the end of the game.
    /// </summary>
    /// <param name="win">Whether the player won the game.</param>
    private void HandleGameOver(bool win)
    {
        mainGameOver.Invoke(win);
        string sfx = win ? "GameWin" : "MinigameLose";
        SFXManager.Instance.Play(sfx);
        gameEndText.text = win ? gameWonBlurb : gameLostBlurb;
        gameOverPanel.SetActive(true);  
    }

    /// <summary>
    /// Reset the game state.
    /// </summary>
    public void ResetGame()
    {
        BlockInput(false);
        Reputation = 50;
        reset.Invoke();
        gameOverPanel.SetActive(false);
        UpdateUI();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BlockInput(bool block)
    {
        inputBlocker.SetActive(block);
    }
}

