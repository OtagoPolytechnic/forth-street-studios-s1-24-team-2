// this class acts as a wrapper for individual minigame prefabs
// on start it finds the camera among its children and sets it as the minigame camera
// it has an initialise method to set camera output to minigame render texture, and call the minigame's reset method
// the reset method is assigned to a unity event in the editor

using UnityEngine;
using UnityEngine.Events;

public class Minigame : MonoBehaviour
{
    public Camera minigameCamera;
    public RenderTexture minigameOutputTexture;
    public UnityEvent Reset;
    public UnityEvent MinigameStart; 

    void Start()
    {
        //minigameCamera = GetComponentInChildren<Camera>();
    }

    public void Initialise()
    {
        minigameCamera.targetTexture = minigameOutputTexture;
        Reset.Invoke();
    }
}
