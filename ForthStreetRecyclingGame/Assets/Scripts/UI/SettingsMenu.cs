using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject perfCanvas;
    [SerializeField] private Toggle perfToggle;

    private const float FPS_INTERVAL = 0.5f;

    void Start()
    {
        perfCanvas.SetActive(false);
        perfToggle.onValueChanged.AddListener(ToggleValueChange);
        perfToggle.isOn = false;
    }

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

    private IEnumerator UpdateFPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(FPS_INTERVAL);
            perfCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "FPS:" + (int)(1.0f / Time.deltaTime);
        }
    }
}
