/*
 * File: MinigameSelector.cs
 * Purpose: Creates a UI element for selecting minigames
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used to create radio buttonst for selecting minigames
/// This is used in the debug panel
/// </summary>
public class MinigameSelector : MonoBehaviour
{
    [SerializeField] private GameObject minigameTogglePrefab; // The prefab for the radio button
    private MinigameLauncher minigameLauncher;
    private List<Minigame> minigames = new List<Minigame>();
    private List<Toggle> toggles = new List<Toggle>();

    /// <summary>
    /// Create the radio buttons for each minigame
    /// </summary>
    void Start()
    {
        minigameLauncher = MinigameLauncher.instance;
        ToggleGroup toggleGroup = GetComponent<ToggleGroup>();
        // Find all the minigames in the scene
        minigames.AddRange(FindObjectsOfType<Minigame>());
        foreach (Minigame minigame in minigames)
        {
            // Create the toggle from the prefab
            GameObject radioButton = Instantiate(minigameTogglePrefab);
            Toggle toggle = radioButton.GetComponent<Toggle>();
            toggle.group = toggleGroup;
            // Add the radio button to the parent
            radioButton.transform.SetParent(transform, false);

            // set the button to be off
            toggle.isOn = false;

            // Set the text of the radio button by finding the label and setting the text
            Text text = radioButton.transform.Find("Label").GetComponent<Text>();
            text.text = minigame.minigameName;

            // Set the onValueChanged listener
            toggle.onValueChanged.AddListener((bool value) =>
            {          
                if (value == true)
                {
                    minigameLauncher.LaunchMinigame(minigame);
                    toggleGroup.allowSwitchOff = false;
                }
            });

            // Add the toggle to the list of toggles
            toggles.Add(toggle);
        }
    }
}