using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXDebugButtonCreator : MonoBehaviour
{
    [SerializeField] private GameObject sfxButtonContainer;
    [SerializeField] private GameObject sfxButtonPrefab;


    // create a button for each sound effect
    // the button is not a prefab, just a generic button
    public void CreateSFXButtons()
    {
        if (sfxButtonContainer == null) return;

        foreach (AudioInfo audioInfo in SFXManager.Instance.AudioInfos)
        {
            // create a button
            GameObject newButton = Instantiate(sfxButtonPrefab, sfxButtonContainer.transform);
            // set the button text to the name of the sound effect
            newButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = audioInfo.name;
            // set the button on click to play the sound effect
            newButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SFXManager.Instance.Play(audioInfo.name));
        }
    }

}
