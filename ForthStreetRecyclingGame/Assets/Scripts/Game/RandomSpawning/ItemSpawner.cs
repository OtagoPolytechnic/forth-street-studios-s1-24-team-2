using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Transform spawnArea;
    public float spawnInterval = 2.0f;
    public float spawnIntervalDecrease = 0.1f;
    public float minSpawnInterval = 0.5f;

    private float currentSpawnTime;
    private float timer;

    void Start()
    {
        currentSpawnTime = spawnInterval;
        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= currentSpawnTime)
        {
            SpawnItem();
            timer = 0.0f;
            currentSpawnTime = Mathf.Max(minSpawnInterval, currentSpawnTime - spawnIntervalDecrease);
        }
    }

    void SpawnItem()
    {
        List<GameObject> selectedPool = RandomlySelectPool();
        GameObject item = ItemPoolManager.Instance.GetPooledObject(selectedPool);

        if (item != null)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
                spawnArea.position.y,
                Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2, spawnArea.position.z + spawnArea.localScale.z / 2)
            );

            Quaternion spawnRotation = Quaternion.Euler(
                Random.Range(0, 360),
                Random.Range(0, 360),
                Random.Range(0, 360)
            );

            item.transform.position = spawnPosition;
            item.transform.rotation = spawnRotation;
            item.SetActive(true);
        }
    }

    private List<GameObject> RandomlySelectPool()
    {
        // Randomly select between recycle and rubbish pool
        List<GameObject> selectedPool = Random.value < 0.5f ?
            ItemPoolManager.Instance.recycleObjectPool :
            ItemPoolManager.Instance.rubbishObjectsPool;

        return selectedPool;
    }
}
