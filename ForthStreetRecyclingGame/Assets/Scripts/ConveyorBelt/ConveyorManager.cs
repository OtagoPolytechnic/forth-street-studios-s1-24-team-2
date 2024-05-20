/*
 *    File: ConveyorManager.cs
 * Purpose: Handles updating of speeds for ConveyorBelt triggers in scene
 *          Pauses objects from moving on minigame load
 *          Objects move again after minigame has ended
 *  Author: Devon
 */

using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    [SerializeField] private ConveyorBelt farConveyor;
    [SerializeField] private ConveyorBelt closeConveyor;
    [SerializeField] private float farSpeed = 1f;
    [SerializeField] private float closeSpeed = 1f;
    public bool minigame = false; //accessed by MinigameLauncher on minigame load/gameover

    /// <summary>
    /// Updates the speed of both conveyors depending if minigame is loaded
    /// </summary>
    private void Update()
    {
        UpdateConveyorSpeeds();
    }

    /// <summary>
    /// Controls the start/stop of conveyor belts on minigame load
    /// </summary>
    private void UpdateConveyorSpeeds()
    {
        if (!minigame) //Main game is loaded
        {
            farConveyor.speed = farSpeed;
            closeConveyor.speed = closeSpeed;
        }
        else // Stop conveyors whle minigame is active
        {
            farConveyor.speed = 0;
            closeConveyor.speed = 0;
        }
    }
}