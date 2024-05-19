// <remarks>
// Author: Erika Stuart
// Date Modified: 20/05/2024
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
    [SerializeField] private GameObject perfCanvas;
    [SerializeField] private Toggle perfToggle;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject mainMenuCanvas;
    public AudioMixer audioMixer;

    private const float FPS_INTERVAL = 0.5f;

    void Start()
    {
        perfCanvas.SetActive(false);
        perfToggle.onValueChanged.AddListener(ToggleValueChange);
        perfToggle.isOn = false;
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
        mainMenuCanvas.SetActive(true);
    }

    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("MainVol", Mathf.Log10(sliderValue) * 20);
    }
    
}
