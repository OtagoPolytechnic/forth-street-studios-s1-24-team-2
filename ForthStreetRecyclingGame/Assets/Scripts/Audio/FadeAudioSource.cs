/// <summary>
/// This class is used to fade the volume of an audiosource.
/// From https://johnleonardfrench.com/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best/
/// To call use: StartCoroutine(FadeAudioSource.StartFade(AudioSource audioSource, float duration, float targetVolume));
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class FadeAudioSource {
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        Debug.Log("Fading from " + start + " to " + targetVolume);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        // stop the audio source
        if (targetVolume <= 0.01)
        {
            audioSource.Stop();
        }
        yield break;
    }



}