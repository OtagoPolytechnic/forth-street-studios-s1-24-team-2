/*
 * File: MinigameObjectManager.cs
 * Purpose: Turns on and off minigame objects
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This struct is used to store a reference to a minigame and its parent game object
/// </summary>
[Serializable]
public struct MinigameObjectComposition
{
    public GameObject gameObject;
    public Minigame minigame;
}

/// <summary>
/// This class is used to turn on and off minigame objects
/// </summary>
public class MinigameObjectManager : MonoBehaviour
{
    // list of minigame object compositions, these should be assigned in the inspector
    public List<MinigameObjectComposition> minigameObjectCompositions = new();
    private Minigame lastRandomMinigame;

    #region Singleton
    public static MinigameObjectManager instance;

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
    /// Find the minigame object to be active or inactive
    /// </summary>
    /// <param name="minigame">The minigame to be set as active or inactive</param>
    /// <param name="active">Whether the minigame object should be active or inactive</param>
    public void SetActive(Minigame minigame, bool active = true)
    {
        foreach (MinigameObjectComposition minigameObjectComposition in minigameObjectCompositions)
        {
            if (minigameObjectComposition.minigame == minigame)
            {
                minigameObjectComposition.gameObject.SetActive(active);
            }
        }
    }

    public Minigame GetRandomMinigame()
    {
        Minigame game = minigameObjectCompositions[UnityEngine.Random.Range(0, minigameObjectCompositions.Count)].minigame;
        if (minigameObjectCompositions.Count > 1 && game == lastRandomMinigame)
        {
            return GetRandomMinigame();
        }
        lastRandomMinigame = game;
        return game;
    }

}