using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidWiggleManager : Minigame
{

    #region Singleton
    // Singleton pattern
    public static LidWiggleManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    public override void Reset()
    {
    }
}
