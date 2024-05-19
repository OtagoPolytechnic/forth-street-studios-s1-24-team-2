using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject perfCanvas;
    [SerializeField] private Toggle perfToggle;
    // Start is called before the first frame update
    void Start()
    {
        perfCanvas.SetActive(false);
        perfToggle.onValueChanged.AddListener(ToggleValueChange);
        perfToggle.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void ToggleValueChange(Toggle change)
    // {
    //     perfCanvas.SetActive(value);
    //     perfCanvas.GetComponentInChildren<Text>().text = "FPS:" + (int)(1.0f / Time.deltaTime);
    // }

    void ToggleValueChange(bool value)
    {
        if (value)
        {
            perfCanvas.SetActive(true);
            perfCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "FPS:" + (int)(1.0f / Time.deltaTime);
        }
        else
        {
            perfCanvas.SetActive(false);
        }
    }
}
