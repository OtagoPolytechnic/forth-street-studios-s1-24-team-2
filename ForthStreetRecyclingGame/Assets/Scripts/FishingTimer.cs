using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FishingTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private float timeLeft = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0) // if time runs out
        {
            GameOver(); // game over
        }
    }

    public IEnumerator StartTimer()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(0.1f); // goes down every 0.1 seconds
            timeLeft -= 0.1f;
            timerText.text = timeLeft.ToString("F1"); //Shows one decimal point

            if (timeLeft <= 5.1)
            {
                timerText.color = Color.red; // warning sign
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        StopCoroutine(StartTimer());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reloads scene (temporary)
    }
}
