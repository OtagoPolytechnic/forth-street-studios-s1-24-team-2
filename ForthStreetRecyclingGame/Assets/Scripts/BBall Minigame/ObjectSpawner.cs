/*
 * This script is currently just rough ideas and 
 * brainstorming how to make the random spawns of
 * items feel fair to the player. None of this 
 * code 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //At least 
    public List<GameObject> recycling;
    public List<GameObject> rubbish;

    public GameObject lastSpawned;
    public GameObject nextSpawned;

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
            //Select next item to spawn (ensure different to first item)
            //Set itemSpawned to true
        }

        if (tb.bottleThrown == true)
        {
            //Wait for .5 seconds and spawn another item
        }
    }

    void RandomObjectChoice(GameObject lastSpawned)
    {
        switch (lastSpawned)
        {
            case recycling[0].gameObject:
            case recycling[1].gameObject:
            case recycling[2].gameObject:
                //increase chance to spawn in rubbish
                break;

            case rubbish[0].gameObject:
            case rubbish[1].gameObject:
            case rubbish[2].gameObject:
                //increase chance to spawn in recycling
                break;
        }

        //Randomly spawn from list based on spawn chance
    }
}
