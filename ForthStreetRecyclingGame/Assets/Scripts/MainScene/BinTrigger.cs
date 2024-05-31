using UnityEngine;
public enum WasteType
{
    Blue,
    Yellow,
    Red
}

public class BinTrigger : MonoBehaviour
{
    private MinigameLauncher minigameLauncher;
    private MainGameManager mainGameManager;
    public WasteType wasteType;

    void Start()
    {
        minigameLauncher = MinigameLauncher.instance;
        mainGameManager = MainGameManager.instance;
    }

    void OnTriggerEnter(Collider other)
    {
        bool correctBin = other.gameObject.CompareTag(wasteType.ToString());

        mainGameManager.HandleWastePlacement(correctBin);
        other.gameObject.SetActive(false);
    }
        
    
}