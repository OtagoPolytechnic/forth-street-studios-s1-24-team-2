using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    // Start is called before the first frame update
    void Start()
    {
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void SettingsButton()
    {
        settings.SetActive(true);
    }
}
