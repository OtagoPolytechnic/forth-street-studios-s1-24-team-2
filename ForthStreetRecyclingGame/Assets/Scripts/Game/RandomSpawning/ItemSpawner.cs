using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Transform spawnArea;
    public float spawnInterval = 5.0f;
    public float spawnIntervalDecrease = 0.02f;
    public float minSpawnInterval = 1.5f;

    private float currentSpawnTime;
    private float timer;

    void Start()
    {
        currentSpawnTime = spawnInterval;
        timer = 0f;
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
            Vector3 spawnPosition = GetRandomSpawnPosition();
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

    Vector3 GetRandomSpawnPosition()
    {
        // Get a random point within the spawn area
        Vector3 randomPoint = new Vector3(
            Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
            spawnArea.position.y,
            Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2, spawnArea.position.z + spawnArea.localScale.z / 2)
        );

        // Ensure the random point is within the spawn area's collider bounds
        Collider spawnCollider = spawnArea.GetComponent<Collider>();
        if (spawnCollider != null)
        {
            randomPoint = RandomPointInBounds(spawnCollider.bounds);
        }

        return randomPoint;
    }

    Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
