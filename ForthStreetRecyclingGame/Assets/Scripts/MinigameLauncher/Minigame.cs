/*
 * File: Minigame.cs
 * Purpose: Abstract class for minigames
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// All the individual minigames will inherit from this class.
/// It defines shared behaviour needed for the MinigameLauncher to work.
/// </summary>
public abstract class Minigame : MonoBehaviour
{
    public string minigameName; // the name of the minigame, should be assigned in awake
    public Camera minigameCamera;   // the camera used for the minigame, should be assigned in the inspector
    public UnityEvent<bool> OnGameOver = new UnityEvent<bool>();    // this event is fired when the minigame is over
    public bool gameStarted;
    protected bool success;

    [SerializeField]private Score scoreScript;

    /// <summary>
    /// Reset the minigame state.
    /// All minigames should implement this method.
    /// </summary>
    public abstract void Reset();

    /// <summary>
    /// Begin the minigame (start timers, etc.)
    /// Not all minigames will need to implement this method.
    /// </summary>
    public virtual void MinigameBegin() 
    { 
        gameStarted = true; 
    }

    protected IEnumerator WaitThenGameOver()
    {
        string sfxString = success ? "MinigameWin" : "MinigameLose";
        SFXManager.Instance.Play(sfxString);
        yield return new WaitForSeconds(MinigameLauncher.instance.GameOverDelay);
        OnGameOver.Invoke(success);
        gameStarted = false;

        if (success)
        {
            scoreScript.AddToScore();
        }
    }

    public void InvokeGameOver()
    {
        StartCoroutine(WaitThenGameOver());
    }

}
