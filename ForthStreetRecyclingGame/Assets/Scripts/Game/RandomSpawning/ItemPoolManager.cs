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
        InitializePool();
    }

    void InitializePool()
    {
        objectPool = new List<GameObject>();
        foreach (GameObject itemPrefab in itemPrefabs)
        {
            for (int i = 0; i < (poolSize / objectPool.Count); i++)
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
