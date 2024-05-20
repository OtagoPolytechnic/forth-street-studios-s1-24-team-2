using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


/// This class is used for controlling the in game music.
/// It is attached to the MusicManager game object.
/// It finds all the audiosources attached to the Musicanager game object and creates a list of AudioInfo objects.
/// When a scene is loaded, it finds the audiosource with the name of the scene and plays it.
/// </summary>
public class MusicManager : MonoBehaviour
{
    #region Singleton
    public static MusicManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This will make the object persist between scenes.
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances.
        }
    }
    #endregion

    [SerializeField] private float fadeTime = 0.5f;
    [HideInInspector] public List<AudioInfo> AudioInfos { get; } = new List<AudioInfo>();

    // This is the initial overall music volume. This is what the music slider in the settings menu is set to when the game starts.
    public float InitMusicLevel = 0.25f;
    private float currentMusicLevel = 0.25f;
    private void Start()
    {

        foreach (AudioSource audioSource in GetComponentsInChildren<AudioSource>())
        {
            AudioInfo newAudioInfo = new AudioInfo(audioSource, audioSource.volume);
            // set the volume of the audiosource to the initial SFX level relative to its initial volume.
            newAudioInfo.Player.volume = (newAudioInfo.InitVolume * InitMusicLevel);
            AudioInfos.Add(newAudioInfo);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        // create debug toggles
        if (AudioDebugManager.Instance != null)
        {
            AudioDebugManager.Instance.CreateMusicToggles();
        }

        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            Play("TitleScreen");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Play(scene.name);
    }

    /// <summary>
    /// This method plays the music with same name as the scene.
    /// </summary>
    /// <param name="sceneName">The name of the scene.</param>
    public void Play(string sceneName)
    {
        // Find the audiosource with supplied name and play it
        foreach (AudioInfo audioInfo in AudioInfos)
        {
            if (audioInfo.name == sceneName)
            {
                // set audio source to init volume  
                audioInfo.Player.volume = audioInfo.InitVolume * currentMusicLevel;
                audioInfo.Player.Play();
            }
            // else if the audiosource is currently playing fade out the music and stop playback.
            else if (audioInfo.Player.isPlaying)
            {
                // start fadeaudiosource coroutine
                StartCoroutine(FadeAudioSource.StartFade(audioInfo.Player, fadeTime, 0f));
            }
        }
    }

    public void FadeOut()
    {
        // Find the audiosource with supplied name and play it
        foreach (AudioInfo audioInfo in AudioInfos)
        {
            if (audioInfo.Player.isPlaying)
            {
                // start fadeaudiosource coroutine
                StartCoroutine(FadeAudioSource.StartFade(audioInfo.Player, fadeTime, 0f));
            }
        }
    }

    /// <summary>
    /// This method is used to change the overall music volume.
    /// </summary>
    /// <param name="volume">The new volume.</param>
    public void ChangeMusicVolume(float volume)
    {
        currentMusicLevel = volume;
        // Change the volume of each audiosource relative to its initial volume
        foreach (AudioInfo audioInfo in AudioInfos)
        {
            audioInfo.Player.volume = (audioInfo.InitVolume * volume);
        }
    }
}
