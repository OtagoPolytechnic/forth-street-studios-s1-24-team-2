
/*
 * File: InGameUI.cs
 * Purpose: Manages in game UI buttons
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

public class InGameUI : MonoBehaviour
{

    [SerializeField] private InstructionMenu instructionMenu; 
    // assign the the settings canvas
    [SerializeField] private GameObject settings;

    // assign the instruction canvas
    [SerializeField] private GameObject instructions;
    
    // assign the close, helpo, and settings buttons
    public void CloseButton()
    {

        Application.Quit();
    }

    public void HelpButton()
    {
        Time.timeScale = 0;
        instructions.SetActive(true);
        instructionMenu.LoadFromMenu();
        gameObject.SetActive(false);
    }

    public void SettingsButton()
    {
        Time.timeScale = 0;
        settings.SetActive(true);
        gameObject.SetActive(false);
    }
}