/*
 *    File: ItemPoolManager.cs
 * Purpose: Creates and stores lists of rubbish/recycling prefabs 
 *          Prefabs instantiated and set inactive for 'spawning' during main scene gameplay
 *  Author: Devon
 *  
 *  Contributions: Help from CHATGPT for getting a random selection of inactive pooled objects (in GetPooledObject)
 */

using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManager : MonoBehaviour
{
    [Header("ItemPoolManager Instance")] 
    [SerializeField] public static ItemPoolManager Instance;

    [Header("Spawned Items Parent Objects")] //Parent objects in scene to tidy heirarchy of pooled objects
    [SerializeField] private Transform recycleParent;
    [SerializeField] private Transform rubbishParent;

    [Header("Rubbish/Recycle prefabs")] // Lists of prefabs for gameplay items
    [SerializeField] private List<GameObject> recycleItemPrefabs; 
    [SerializeField] private List<GameObject> rubbishItemPrefabs;

    // Pools of instantiated objects (Rubbish/Recycle)
    // Used in ItemSpawner script
    [HideInInspector] public List<GameObject> recycleObjectPool; 
    [HideInInspector] public List<GameObject> rubbishObjectsPool;
    private int poolSizeMax = 20; //Max limit of any given item type spawned in scene

    /// <summary>
    /// Initialises instance and the object pools
    /// </summary>
    void Awake()
    {
        Instance = this;
        InitialisePool();
    }

    /// <summary>
    /// Initilise empty object pool lists and call to instantiate both from list of prefabs
    /// </summary>
    void InitialisePool()
    {
        recycleObjectPool = new List<GameObject>();
        rubbishObjectsPool = new List<GameObject>();

        InstantiateObjects(recycleObjectPool, recycleItemPrefabs, recycleParent);
        InstantiateObjects(rubbishObjectsPool, rubbishItemPrefabs, rubbishParent);
    }

    /// <summary>
    /// Instantiates objects for the pool based on the provided prefabs.
    /// </summary>
    /// <param name="pool">Object pool being populated</param>
    /// <param name="itemPrefabs">List containing prefabs for recycle/rubbish game objects</param>
    /// <param name="parent">Parent object for the spawned objects</param>
    void InstantiateObjects(List<GameObject> pool, List<GameObject> itemPrefabs, Transform parent)
    {
        //Limit the max number of any given item spawning to below the pool size max
        //poolSizeMax = 20; itemPrefabs = 4; 5 of each item will be instantiated
        int maxSpawnCount = poolSizeMax / itemPrefabs.Count;

        //Go through each list of prefabs and instantiate to a pool list
        foreach (GameObject itemPrefab in itemPrefabs)
        {
            for (int i = 0; i < maxSpawnCount; i++) //Limit number of items that will spawn for each prefab
            {
                GameObject obj = Instantiate(itemPrefab, parent); //Instantiate object as child of specified parent object
                obj.SetActive(false); //Hide object until 'spawned' during gameplay
                pool.Add(obj);
            }
        }
    }

    /// <summary>
    /// Returns a random inactive object from the specified pool or null if none are available.
    /// </summary>
    /// <param name="pool">List of initilised pooled objects to pull from</param>
    /// <returns>Item to spawn at random position/rotation (In ItemSpawner.cs)</returns>
    public GameObject GetPooledObject(List<GameObject> pool)
    {
        //List to store all inactive objects in the pool
        List<GameObject> inactiveObjects = new List<GameObject>();

        //Go through each object and check if active
        //If inactive, add to list
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                inactiveObjects.Add(obj);
            }
        }

        //Get a random item from the inactive objects list
        if (inactiveObjects.Count > 0)
        {
            int randomIndex = Random.Range(0, inactiveObjects.Count);
            return inactiveObjects[randomIndex];
        }

        return null;
    }
}
