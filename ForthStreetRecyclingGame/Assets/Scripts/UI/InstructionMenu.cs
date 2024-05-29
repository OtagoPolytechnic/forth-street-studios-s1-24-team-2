using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionMenu : MonoBehaviour
{
    [SerializeField] private GameObject instructionsCanvas;
    [SerializeField] private GameObject mainMenuCanvas;

    // <summary>
    // The button that returns to the main menu screen
    // </summary>
    public void BackButton()
    {
        instructionsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
