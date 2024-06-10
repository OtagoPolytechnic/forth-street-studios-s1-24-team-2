/*
 *    File: ItemDespawn.cs
 * Purpose: Sets spawned objects back to inactive
 *          Allows for objects to be respawned from inactive items pool
 *  Author: Devon
 */

using UnityEngine;

public class ItemDespawn : MonoBehaviour
{
    private MainGameManager mainGameManager;
    void Start()
    {
        mainGameManager = MainGameManager.instance;
    }

    void OnTriggerEnter(Collider other) //Recycle or rubbish collider
    {
        other.gameObject.SetActive(false);
        mainGameManager.HandleWastePlacement(correct:false);
    }
}