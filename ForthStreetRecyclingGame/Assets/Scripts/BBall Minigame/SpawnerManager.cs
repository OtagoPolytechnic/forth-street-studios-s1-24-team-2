using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [Header("Spawnable GameObjects")] // Lists to hold object prefabs
    public List<GameObject> recycling = new List<GameObject>();
    public List<GameObject> rubbish = new List<GameObject>();

    [Header("Spawn Status")] //Check if spawn needed / spawn first item without delay
    public bool spawning; 
    public bool canSpawn;
    private bool firstSpawn;
    public GameObject spawnedItem;

    public GameManager manager; //Uses isGameOver from script to stop spawning at game end
    private int shotsRemaining; //Localised storage of value in gamemanager script

    /// <summary>
    /// Get reference to gamemanager to control spawning
    /// </summary>
    private void Start()
    {
        manager = GameObject.Find("Managers/GameManager").GetComponent<GameManager>(); //Get GameManager script from scene
        SetVariables();
    }

    /// <summary>
    /// Set initial values for game start/restart
    /// </summary>
    public void SetVariables()
    {
        spawning = false;
        firstSpawn = true;
        canSpawn = true;
        shotsRemaining = manager.shotsRemaining;
        canSpawn = !manager.isGameOver;
    }

    private void Update()
    {
        shotsRemaining = manager.shotsRemaining;

        //If game is still running (no end game panels showing)
        if (canSpawn) 
        {
            //If nothing is spawning and player still has shots left, start coroutine to choose/spawn a new object
            if (!spawning && shotsRemaining > 0) 
            {
                StartCoroutine(SpawnObjectWithDelay());
            }
        }
        else
        {
            Destroy(spawnedItem); //Destroy item if spawned during game over screens
        }
    }

    /// <summary>
    /// Spawn object with a delay 0.5 second delay  <br />
    /// First object spawn without this delay
    /// </summary>
    /// <returns>WaitForSeconds used for 0.5s delay</returns>
    IEnumerator SpawnObjectWithDelay()
    {
        spawning = true; //Currently spawning an object (stops update constantly spawning new items)

        //Only use delay for spawns after first item (removes initial 0.5s wait on minigame start)
        if (!firstSpawn) 
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            firstSpawn = false;
        }

        // Generate a random integer (0,1) for object list selection
        int randomIndex = Random.Range(0, 2);

        // Generate a random rotation for axis
        Quaternion randomRotation = Quaternion.Euler(Random.Range(0, 90), Random.Range(70, 110), Random.Range(0, 90));

        // Following code by Johnathan in Code Review
        // Choose the list to use based on randomIndex
        List<GameObject> chosenList = randomIndex == 0 ? recycling : rubbish;

        // Randomly select an object from the chosen list and instantiate (with y rotation of 90deg)
        int chosenIndex = Random.Range(0, chosenList.Count);
        GameObject obj = Instantiate(chosenList[chosenIndex], transform.position, randomRotation);
        spawnedItem = obj;
    }
}