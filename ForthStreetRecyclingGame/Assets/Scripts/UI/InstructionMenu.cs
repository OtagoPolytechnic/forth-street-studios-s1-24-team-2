/* 
 * File: InstructionMenu.cs
 * Purpose: Controls the toggling of menu on/off from titlescreen/back button
 *          Holds a list of panels that toggle visibility when next/prev buttons clicked
 *          When end of list reached, wrap around to first panel
 *          Loads the main scene instructions as the default panel (instructions[0])
 *          Enable/Disable panel used to remove some repetitive code
 * Author: Devon
 */

using System.Collections.Generic;
using UnityEngine;

public class InstructionMenu : MonoBehaviour
{
    [Header("Canvas Objects")]
    [SerializeField] private GameObject instructionsCanvas;
    [SerializeField] private GameObject mainMenuCanvas;

    [Header("Instruction Panels")]
    [SerializeField] private List<GameObject> instructions; //Panels with instructions
    private int currentPanelIndex = 0; //Current panel to show

    /// <summary>
    /// Sets the default panel to show on main menu button click
    /// </summary>
    public void LoadFromMenu()
    {
        currentPanelIndex = 0; //Reset index so maingame instruction always shows on first load
        EnablePanel(currentPanelIndex);
    }

    // <summary>
    // The button that returns to the main menu screen
    // </summary>
    public void BackButton()
    {
        instructionsCanvas.SetActive(false); //Hide the instruction menu
        DisablePanel(currentPanelIndex); //Hide the panel so it doesnt overlap on reload of canvas
        mainMenuCanvas.SetActive(true); //Show the main menu
    }

    // <summary>
    // Move to the next instruction panel
    // </summary>
    public void NextInstruction()
    {
        DisablePanel(currentPanelIndex);
        GetNewIndex(1); //Get the next index
        EnablePanel(currentPanelIndex);
    }

    // <summary>
    // Move to the previous instruction panel
    // </summary>
    public void PreviousInstruction()
    {
        DisablePanel(currentPanelIndex);
        GetNewIndex(-1); //Get previous index
        EnablePanel(currentPanelIndex);
    }

    /// <summary>
    /// Sets panel to inactive
    /// </summary>
    /// <param name="index">Index in instructions list to toggle off</param>
    private void DisablePanel(int index)
    {
        instructions[index].SetActive(false); //Hide panel at specified index
    }

    /// <summary>
    /// Updates the current list index depending on passed value
    /// </summary>
    /// <param name="value">Direction in list to move (+1 -> up list, -1 -> down list)</param>
    private void GetNewIndex(int value) //List index increase/decrease value
    {
        currentPanelIndex = (currentPanelIndex + value + instructions.Count) % instructions.Count; // Increment/Decrement the index and wrap around if at the start/end of the list
    }

    /// <summary>
    /// Sets panel to active
    /// </summary>
    /// <param name="index">Index in instructions list to toggle on</param>
    private void EnablePanel(int index)
    {
        instructions[index].SetActive(true); //Show panel at specified index
    }
}