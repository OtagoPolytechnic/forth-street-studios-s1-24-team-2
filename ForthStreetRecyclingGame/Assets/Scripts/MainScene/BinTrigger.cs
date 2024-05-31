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
    public WasteType wasteType;

    void Start()
    {
        minigameLauncher = MinigameLauncher.instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(wasteType.ToString()))
        {
            Debug.Log("Correct bin");
        }
        else
        {
            Debug.Log("Wrong bin");
        }

        other.gameObject.SetActive(false);
        minigameLauncher.LaunchRandomMinigame();
    }

}