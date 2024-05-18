/*
 *    File: ConveyorManager.cs
 * Purpose: Handles updating of speeds for ConveyorBelt triggers in scene
 *  Author: Devon
 */

using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    [SerializeField] private ConveyorBelt farConveyor;
    [SerializeField] private ConveyorBelt closeConveyor;
    [SerializeField] private float farSpeed = 1f;
    [SerializeField] private float closeSpeed = 1f;
    public bool minigame = false; //accessed by gamemanager on minigame load

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
    public void UpdateConveyorSpeeds()
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
