/*
 *    File: ItemSpawner.cs
 * Purpose: Controls the spawning of instantiated game objects by setting them active
 *          Updates the position and rotation of spawned object to an area at the top of the far conveyor
 *  Author: Devon
 *  
 *  Contributions: Help from CHATGPT for getting random spawn positions within the trigger collider area
 */

using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Spawn Area Trigger Collider")]
    [SerializeField] private Collider spawnArea; // Reference to the spawn area collider where items can spawn

    [Header("Item Spawn Settings")]
    [SerializeField] private float spawnInterval; // Initial time interval between item spawns
    [SerializeField] private float spawnIntervalDecrease; // Amount to decrease the spawn interval after a certain number of items are spawned
    [SerializeField] private float minSpawnInterval; // Minimum possible spawn interval
    [SerializeField] private float currentSpawnInterval; // Current time interval between spawns

    [Header("Item Spawn Variables")]
    private float timer; // Timer to track elapsed time since last item spawn
    private int itemsSpawned; //Count of spawned items

    [Header("Minigame Status")]
    public bool minigame; //Check if minigame is loaded for spawning any new items (Updates in MinigameLauncher.cs)

    [Header("Constant Values")]
    private const int maxAngle = 360;
    private const int itemsBeforeDecrease = 5;
    private const float halfValue = 0.5f;

    private MainGameManager mainGameManager;
    /// <summary>
    /// Set initial variable values and spawn first item without delay
    /// </summary>
    void Start()
    {
        mainGameManager = MainGameManager.instance;
        // consume MainGameOver from MainGameManager
        mainGameManager.mainGameOver.AddListener(HandleGameOver);
        mainGameManager.reset.AddListener(HandleReset);
        spawnInterval = 5f;
        spawnIntervalDecrease = 0.5f;
        minSpawnInterval = 1.5f;
        currentSpawnInterval = spawnInterval;
        timer = 0f;
        itemsSpawned = 0;
        minigame = false;
        SpawnItem();
    }

    /// <summary>
    /// Spawns game objects after an interval    <br />
    /// Decreases spawn delay every 5 items spawned
    /// </summary>
    void Update()
    {
        if (!minigame) //Check if no minigame is loaded before spawning
        {
            timer += Time.deltaTime;
            if (timer >= currentSpawnInterval) //Check enough time has passed between spawns
            {
                SpawnItem();
                timer = 0.0f;

                //Decrease spawn interval every 5 items spawned
                if (itemsSpawned % itemsBeforeDecrease == 0)
                {
                    //Find the current interval between spawns
                    //Will decrease current interval unless it is below minimum spawn interval
                    currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalDecrease);
                }
            }
        }
    }

    /// <summary>
    /// Randomly selects a pool of objects to spawn from. Item it randomly chosen from inactive objects in pool
    /// Item spawns with random position and rotation
    /// </summary>
    void SpawnItem()
    {
        List<GameObject> selectedPool = RandomlySelectPool(); //Select either recycle or rubbish object pools randomly
        GameObject item = ItemPoolManager.Instance.GetPooledObject(selectedPool); //Retrieve a single inactive item from the pool

        if (item != null) //Check an available item was taken from object pool
        {
            Vector3 spawnPosition = GetRandomSpawnPosition(); //Get a random spawn position
            Quaternion spawnRotation = Quaternion.Euler(Random.Range(0, maxAngle), Random.Range(0, maxAngle), Random.Range(0, maxAngle)); //Get a random rotation

            //Set the item transform and set active so player can see
            item.transform.position = spawnPosition;
            item.transform.rotation = spawnRotation;
            item.SetActive(true);
        }

        itemsSpawned++; //Update total items spawned (controls respawn delay interval decreasing)
    }

    /// <summary>
    /// Randomly selects between the recycle and rubbish object pools.
    /// </summary>
    /// <returns>A list of pooled game objects</returns>
    // ChatGPT used to shorten if-else code
    private List<GameObject> RandomlySelectPool()
    {
        // Randomly select between recycle and rubbish pool
        List<GameObject> selectedPool = Random.value < halfValue ?
            ItemPoolManager.Instance.recycleObjectPool :
            ItemPoolManager.Instance.rubbishObjectsPool;

        return selectedPool;
    }

    /// <summary>
    /// Gets a random position within the spawn area collider bounds
    /// </summary>
    /// <returns>Vector3 representing the random spawn position.</returns>
    Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPoint = Vector3.zero; // Generate a empty position to overwrite

        // Ensure the random point is within the spawn area's collider bounds
        if (spawnArea != null)
        {
            randomPoint = RandomPointInBounds(spawnArea.bounds); //Set spawn position within collider bounds
        }

        return randomPoint;
    }

    /// <summary>
    /// Generates a random point within the given bounds
    /// </summary>
    /// <param name="bounds">The bounds within which to generate the random point</param>
    /// <returns>A Vector3 representing the random point within the bounds</returns>
    // ChatGPT helped in understanding collider bounds
    Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private void HandleGameOver(bool success) => enabled = false;
    private void HandleReset()
    {
        // log this
        Debug.Log("Resetting item spawner");
        enabled = true;
        itemsSpawned = 0;
        currentSpawnInterval = spawnInterval;
        timer = 0f;
        SpawnItem(); // spawn an item immediately
    }
}
