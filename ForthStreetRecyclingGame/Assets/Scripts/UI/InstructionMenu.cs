using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionMenu : MonoBehaviour
{
    [SerializeField] private GameObject instructionsCanvas;
    [SerializeField] private GameObject mainMenuCanvas;

    [SerializeField] private List<GameObject> instructions; // Assuming instructions are GameObjects with TMPro panels

    private int currentPanelIndex = 0;

    private void Start()
    {
        // Initialize by showing the first panel and hiding the others
        ShowPanel(currentPanelIndex);
    }

    // <summary>
    // The button that returns to the main menu screen
    // </summary>
    public void BackButton()
    {
        instructionsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    // <summary>
    // Move to the next instruction panel
    // </summary>
    public void NextInstruction()
    {
        // Hide the current panel
        instructions[currentPanelIndex].SetActive(false);

        // Increment the index and wrap around if at the end of the list
        currentPanelIndex = (currentPanelIndex + 1) % instructions.Count;

        // Show the next panel
        ShowPanel(currentPanelIndex);
    }

    // <summary>
    // Move to the previous instruction panel
    // </summary>
    public void PreviousInstruction()
    {
        // Hide the current panel
        instructions[currentPanelIndex].SetActive(false);

        // Decrement the index and wrap around if at the beginning of the list
        currentPanelIndex = (currentPanelIndex - 1 + instructions.Count) % instructions.Count;

        // Show the previous panel
        ShowPanel(currentPanelIndex);
    }

    // <summary>
    // Show the panel at the specified index
    // </summary>
    private void ShowPanel(int index)
    {
        if (index >= 0 && index < instructions.Count)
        {
            instructions[index].SetActive(true);
        }
    }
}