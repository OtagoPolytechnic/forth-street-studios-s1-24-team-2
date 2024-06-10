using UnityEngine;

/// <summary>
/// Snaps the item to the bin when the mouse enters the bin
/// </summary>
public class SnapItemToBin : MonoBehaviour
{
    private Vector3 wayPoint;
    private float wpYOffset;
    public GameObject heldItem;
    private DragObject dragObject; //script reference to the drag object
    private string binType;
    //[SerializeField] private GameObject minigameResult;
    //private MinigameResult minigameResultScript;

    public MinigameLauncher minigameLauncher;
    private Minigame testGame;

    /// <summary>
    /// Gets scripts and objects when the scene first loads
    /// </summary>
    void Start()
    {
        dragObject = GameObject.Find("PickupObjects").GetComponent<DragObject>(); //finds the pickup object and gets the script
        wayPoint = transform.GetChild(0).gameObject.transform.position; //gets the first child of the bin
        wpYOffset = 0.75f; //offsets the item to the bin
        wayPoint.y += wpYOffset; //offsets the item to the bin
        //minigameResultScript = minigameResult.GetComponent<MinigameResult>();

        // find the minigame object has name TestMinigame
        testGame = GameObject.Find("TestMinigame").GetComponent<Minigame>();

        // subscribe to the OnGameOver event
        minigameLauncher.minigameOver.AddListener(HandleGameOver);
    }

    /// <summary>
    /// Handles the game over event
    /// </summary>
    private void HandleGameOver(bool success)
    {
        string message = success ? "You win!" : "You lose!";
        Debug.Log(message);
    }

    /// <summary>
    /// GetMouseButtonDown doesn't work in a method that is a mouse event itself, so it's moved to Update and type is stored
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && binType != null) // if the player clicks a bin
        {
            BinCheck(); // checks the type
        }
    }

    /// <summary>
    /// When the mouse enters the bin, the item is snapped to the bin
    /// </summary>
    void OnMouseEnter()
    {
        if (dragObject.item == null) return; //if the item is null, return (do nothing)
        heldItem = dragObject.item; //gets the item from the drag object
        heldItem.transform.position = wayPoint; //snaps the item to the bin
        heldItem.transform.rotation = Quaternion.Euler(90, 0, 0); //rotates the item to the bin
        binType = gameObject.name;
    }

    /// <summary>
    /// When the mouse exits the bin, the bin type is set to null
    /// so the player can't press mouse button outside of the bin
    /// </summary>
    void OnMouseExit()
    {
        binType = null;
    }

    /// <summary>
    /// Checks the bin type
    /// </summary>
    private void BinCheck()
    {
        switch (binType)
        {
            case "Recycling": // drops the recycling in the bin
                Debug.Log("Recycling");
                binType = "Recycling";
                DropRecycling();
                return;
            case "Rubbish": // whatever gets trashed is gone for good.
                Debug.Log("Rubbish");
                binType = "Rubbish";
                Destroy(heldItem);
                return;
            case "Glass": // same thing as recyling but without checking if its glass or not yet
                Debug.Log("Glass");
                binType = "Glass";
                // Minigame
                return;
        }
    }

    private void DropRecycling()
    {
        heldItem.GetComponent<Rigidbody>().isKinematic = false;

        //  set the minigame as current minigame and launch it
        minigameLauncher.LaunchMinigame(testGame);

        if (true)
        {
            Destroy(heldItem);
        }
        else
        {
            heldItem.transform.position = wayPoint; //snaps the item to the bin
            heldItem.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        
        // Start minigame

    }
}
