using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    public ConveyorBelt far;
    public ConveyorBelt close;
    public int farSpeed;
    public int closeSpeed;
    public bool loadingGame;

    // Start is called before the first frame update
    void Start()
    {
        loadingGame = false;
        farSpeed = 1;
        closeSpeed = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        StartCoroutine(RunConveyors());
    }

    private IEnumerator RunConveyors()
    {
        if (!loadingGame) //Main game is loaded
        {
            far.speed = farSpeed;
            close.speed = closeSpeed;
        }
        else //Minigame is loaded
        {
            far.speed = 0;
            close.speed = 0;
        }
        return null;
    }
}
