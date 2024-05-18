using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManager : MonoBehaviour
{
    [SerializeField] public static ItemPoolManager Instance;
    [SerializeField] private List<GameObject> recycleItemPrefabs;
    [SerializeField] private List<GameObject> rubbishItemPrefabs;

    [HideInInspector] public List<GameObject> recycleObjectPool; // Used in ItemSpawner script
    [HideInInspector] public List<GameObject> rubbishObjectsPool;
    private int poolSize = 20;

    void Awake()
    {
        Instance = this;
        InitialisePool();
    }

    void InitialisePool()
    {
        recycleObjectPool = new List<GameObject>();
        rubbishObjectsPool = new List<GameObject>();

        InstantiateObjects(recycleObjectPool, recycleItemPrefabs);
        InstantiateObjects(rubbishObjectsPool, rubbishItemPrefabs);
    }

    void InstantiateObjects(List<GameObject> pool, List<GameObject> itemPrefabs)
    {
        int maxSpawnCount = poolSize / itemPrefabs.Count;

        foreach (GameObject itemPrefab in itemPrefabs)
        {
            for (int i = 0; i < maxSpawnCount; i++)
            {
                GameObject obj = Instantiate(itemPrefab);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(List<GameObject> pool)
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
}
