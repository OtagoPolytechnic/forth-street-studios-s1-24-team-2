using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManager : MonoBehaviour
{
    public static ItemPoolManager Instance;
    public List<GameObject> itemPrefabs;
    public int poolSize = 20;
    private List<GameObject> objectPool;

    void Awake()
    {
        Instance = this;
        InitialisePool();
    }

    void InitialisePool()
    {
        objectPool = new List<GameObject>();
        int maxOfItem = poolSize/itemPrefabs.Count;
        Debug.Log(maxOfItem);

        foreach (GameObject itemPrefab in itemPrefabs)
        {
            for (int i = 0; i < maxOfItem; i++)
            {
                GameObject obj = Instantiate(itemPrefab);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
}
