using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Sprite[] sprites;
    private float canvasRange = 500;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomPopup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RandomPopup()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5)); //Randomly occurring popups
            Image image = new GameObject("PopupText", typeof(Image)).GetComponent<Image>(); //Create new image
            image.transform.SetParent(canvas.transform); 
            image.rectTransform.anchoredPosition = new Vector2(Random.Range(-canvasRange, canvasRange), Random.Range(-canvasRange, canvasRange)); //Random position
            float randScale = Random.Range(100f, 300f); //Random scale
            image.rectTransform.sizeDelta = new Vector2(randScale, randScale); //Applies the random scale
            image.sprite = sprites[Random.Range(0, sprites.Length)]; //Random sprite

            for (float i = 0; i < 1; i += Time.deltaTime) //Fade in image
            {
                image.color = new Color(1, 1, 1, 0 + i); //Increase alpha
                yield return null;
            }
            
            yield return new WaitForSeconds(1); //Stay on screen for 1 second
            for (float i = 0; i < 1; i += Time.deltaTime) //Fade out image
            {
                image.color = new Color(1, 1, 1, 1 - i); //Reduce alpha
                yield return null;
            }
            Destroy(image.gameObject);
        }
    }
}
