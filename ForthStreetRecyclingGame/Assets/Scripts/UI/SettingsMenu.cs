// <remarks>
// Author: Erika Stuart
// Date Modified: 22/05/2024
// </remarks>
// <summary>
// This script is used to control the settings menu screen
// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private GameObject perfCanvas;
    [SerializeField] private Toggle perfToggle;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private MusicManager musicManager;

    private const float FPS_INTERVAL = 0.5f;

    void Start()
    {
        perfCanvas.SetActive(false);
        perfToggle.onValueChanged.AddListener(ToggleValueChange);
        perfToggle.isOn = false;
        // audioSource = audioSourceObj.GetComponent<AudioSource>();
        // audioSource.Play();
        // audioSource.volume = volumeSlider.value;
        volumeSlider.value = musicManager.InitMusicLevel;
    }

    // <summary>
    // When the performance toggle is changed
    // </summary>
    void ToggleValueChange(bool value)
    {
        if (value)
        {
            perfCanvas.SetActive(true);
            StartCoroutine(UpdateFPS());
        }
        else
        {
            perfCanvas.SetActive(false);
        }
    }

    // <summary>
    // Coroutine so the FPS counter can run independently while playing
    // </summary>
    private IEnumerator UpdateFPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(FPS_INTERVAL);
            perfCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "FPS:" + (int)(1.0f / Time.deltaTime);
        }
    }

    // <summary>
    // The button that returns to the main menu screen
    // </summary>
    public void BackButton()
    {
        settingsCanvas.SetActive(false);
        if (mainMenu.GameStarted)
        {
            Time.timeScale = 1;
        }
        else
        {
            mainMenuCanvas.SetActive(true);
        }
    }

    // public void SetVolume()
    // {
    //     AudioListener.volume = volumeSlider.value; // sets the volume of the audio listener to the value of the slider
    // }
    
}
