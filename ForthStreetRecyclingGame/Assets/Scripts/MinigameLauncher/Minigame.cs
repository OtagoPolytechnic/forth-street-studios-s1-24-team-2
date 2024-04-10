// this class acts as a wrapper for individual minigame prefabs
// on start it finds the camera among its children and sets it as the minigame camera
// it has an initialise method to set camera output to minigame render texture, and call the minigame's reset method
// the reset method is assigned to a unity event in the editor

using UnityEngine;
using UnityEngine.Events;

public abstract class Minigame : MonoBehaviour
{
    public Camera minigameCamera;

    public UnityEvent<bool> OnGameOver = new UnityEvent<bool>();
    // public UnityEvent MinigameStart; 
    public bool success;


    #region Singleton
    // Singleton pattern
    public static Minigame instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public abstract void Reset();
    public virtual void MinigameBegin() { }

}
