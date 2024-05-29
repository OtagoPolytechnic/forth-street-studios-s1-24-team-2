// <remarks>
// Author: Erika Stuart
// Date Modified: 20/05/2024
// </remarks>
// <summary>
// This script is used to control the main menu screen
// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject scoreCanvas;
    [SerializeField] private GameObject instructions;

    void Start()
    {
        settings.SetActive(false);
        Time.timeScale = 0;
        scoreCanvas.SetActive(false);
    }

    // <summary>
    // Button that starts the game and hides the canvas'
    // </summary>
    public void PlayButton()
    {
        MusicManager.Instance.Play("SortingFacility");
        //SceneManager.LoadScene("Main Scene");
        mainMenu.SetActive(false);
        settings.SetActive(false);
        scoreCanvas.SetActive(true);
        Time.timeScale = 1;
    }

    // <summary>
    // Button that opens the settings menu
    // </summary>
    public void SettingsButton()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    /// <summary>
    /// Button that opens the gameplay instructions
    /// </summary>
    public void InstructionsButton()
    {
        instructions.SetActive(true);
        mainMenu.SetActive(false);
    }

    // <summary>
    // Button that quits the game
    // </summary>
    public void QuitButton()
    {
        Application.Quit();
    }
}
