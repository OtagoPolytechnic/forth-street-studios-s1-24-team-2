/*
 *    File: ItemDespawn.cs
 * Purpose: Used to set all triggered gameobjects at the end of the conveyor back to inactive
 *          Allows for objects to be spawned again from inactive object pool
 *  Author: Devon
 *  
 *  Contributions: Help from CHATGPT for getting random spawn positions within the trigger collider area
 */

using UnityEngine;

public class ItemDespawn : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}