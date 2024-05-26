/*
 * File: MainGameManager.cs
 * Purpose: Manage the game state
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// Game manager for tracking the state of the game.
/// </summary>
public class MainGameManager : MonoBehaviour
{
    [SerializeField]
    private int _targetFrameRate = 60;
    
    /// <summary>
    /// Setter and getter for the target frame rate.
    /// </summary>
    public int TargetFrameRate
    {
        get { return _targetFrameRate; }
        set
        {
            _targetFrameRate = value;
            // apply the new target frame rate
            Application.targetFrameRate = _targetFrameRate;
        }
    }

    /// <summary>
    /// Workaround for calling setter logic in the inspector.
    /// </summary>
    void OnValidate()
    {
        TargetFrameRate = _targetFrameRate;
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
        Application.targetFrameRate = _targetFrameRate;
    }

}