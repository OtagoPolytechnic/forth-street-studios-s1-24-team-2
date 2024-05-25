using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameResult : MonoBehaviour
{
    /* 1. snaptobin.cs minigame starts
    *  2. if win
    *     - destroy
    *     - add to score
    *  3. if lose, fly from bin
    *     - might need to include some destroy collider
    */

    private bool hasWon = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetHasWon()
    {
        return hasWon;
    }
}
