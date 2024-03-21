using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> recycling;
    public List<GameObject> rubbish;

    public Vector3 spawnLocation;
    public ThrowBottle tb;
    public bool itemSpawned;

    // Start is called before the first frame update
    void Start()
    {
        tb = GetComponent<ThrowBottle>();
        spawnLocation = new Vector3(-0.6f, 0.42f, 0);
        itemSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckItemSpawn();
    }

    void CheckItemSpawn()
    {
        if (!itemSpawned)
        {
            //Spawn in an item
            //Set itemSpawned to true
        }

        if (tb.bottleThrown == true)
        {
            //Wait for .5 seconds and spawn another item
        }

    }
}
