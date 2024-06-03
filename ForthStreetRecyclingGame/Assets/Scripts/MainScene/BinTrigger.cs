/*
 * File: BinTrigger.cs
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>   
/// Enum for the different types of waste
/// </summary>
public enum WasteType
{
    Blue,
    Yellow,
    Red,
    Green
}

/// <summary>
/// Trigger for the waste bins in the main scene
/// </summary>
public class BinTrigger : MonoBehaviour
{
    private MinigameLauncher minigameLauncher;
    private MainGameManager mainGameManager;
    [SerializeField] private WasteType wasteType;
    [SerializeField] private BinController binController;

    void Start()
    {
        minigameLauncher = MinigameLauncher.instance;
        mainGameManager = MainGameManager.instance;
    }

    void OnTriggerEnter(Collider other)
    {
        // compare object tag with waste type
        bool correctBin = other.gameObject.CompareTag(wasteType.ToString());

        mainGameManager.HandleWastePlacement(correctBin);

        // get pickupable object script
        PickableObject pickableObject = other.gameObject.GetComponent<PickableObject>();

        // reset object scale
        pickableObject.ResetScale();
        
        // deactivate so ItemPoolManager will respawn it
        other.gameObject.SetActive(false);

        // deactivate the bin panel
        binController.ActivatePanel(false);
    }
}