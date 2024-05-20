using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDebugToggleCreator: MonoBehaviour
{
    [SerializeField] private GameObject musicToggleContainer;
    [SerializeField] private GameObject musicTogglePrefab;


    // create a button for each sound effect
    // the button is not a prefab, just a generic button
    public void CreateMusicToggles()
    {
        if (musicToggleContainer == null) return;

        foreach (AudioInfo audioInfo in MusicManager.Instance.AudioInfos)
        {
            // create a toggle that toggles start/stop of the audiosource
            GameObject newToggle = Instantiate(musicTogglePrefab, musicToggleContainer.transform);
            // set the toggle text to the name of the sound effect
            // this is a legacy text not TMP
            newToggle.GetComponentInChildren<UnityEngine.UI.Text>().text = audioInfo.name;
            // set the toggle on click to play the sound effect
            newToggle.GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener((bool value) => toggleMusic(audioInfo.name, value));
            // set the toggle off by default
            newToggle.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
        }
    }

    private void toggleMusic(string name, bool value)
    {
        // Find the audiosource with supplied name and play it
        foreach (AudioInfo audioInfo in MusicManager.Instance.AudioInfos)
        {
            if (audioInfo.name == name)
            {
                if (value)
                {
                    audioInfo.Player.Play();
                }
                else
                {
                    audioInfo.Player.Stop();
                }
                return;
            }
        }
    }
}
