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
    [SerializeField] private int farSpeed = 1;
    [SerializeField] private int closeSpeed = 1;
    public bool minigame = false; //accessed by gamemanager on minigame load

    /// <summary>
    /// Sets initial values for the conveyor speeds
    /// </summary>
    private void Start()
    {
        farConveyor.speed = farSpeed;
        closeConveyor.speed = closeSpeed;
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
