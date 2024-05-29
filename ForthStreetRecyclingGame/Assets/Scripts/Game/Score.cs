using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score;
    [SerializeField]private Slider slider;

    public void AddToScore()
    {
        score++;
        slider.value = score / 5f;

        if (score == 5)
        {
            Debug.Log("You Win!");
        }
    }   
}
