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
    protected bool success;
    protected bool gameStarted;
    [SerializeField] private int gameOverDelay = 1;    // flag to check if the player has won/lost, passed to the OnGameOver event


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
        yield return new WaitForSeconds(gameOverDelay);
        OnGameOver.Invoke(success);
        gameStarted = false;
    }

    protected void InvokeGameOver()
    {
        StartCoroutine(WaitThenGameOver());
    }

}
