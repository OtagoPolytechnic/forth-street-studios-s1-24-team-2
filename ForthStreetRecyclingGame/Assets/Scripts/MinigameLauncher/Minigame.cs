/*
 * File: Minigame.cs
 * Purpose: Abstract class for minigames
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// All the individual minigames will inherit from this class.
/// It defines shared behaviour needed for the MinigameLauncher to work.
/// </summary>
public abstract class Minigame : MonoBehaviour
{
    public Camera minigameCamera;   // the camera used for the minigame, should be assigned in the inspector
    public UnityEvent<bool> OnGameOver = new UnityEvent<bool>();    // this event is fired when the minigame is over
    public bool success;    // flag to check if the player has won/lost, passed to the OnGameOver event

    #region Singleton
    // Singleton pattern
    public static Minigame instance;

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

    /// <summary>
    /// Reset the minigame state.
    /// All minigames should implement this method.
    /// </summary>
    public abstract void Reset();

    /// <summary>
    /// Begin the minigame (start timers, etc.)
    /// Not all minigames will need to implement this method, so it does nothing by default.
    /// </summary>
    public virtual void MinigameBegin() { }

}
