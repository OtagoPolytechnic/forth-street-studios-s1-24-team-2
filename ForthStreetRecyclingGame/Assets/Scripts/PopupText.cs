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
            yield return new WaitForSeconds(Random.Range(1, 5));
            Image image = new GameObject("PopupText", typeof(Image)).GetComponent<Image>();
            image.transform.SetParent(canvas.transform);
            image.rectTransform.anchoredPosition = new Vector2(Random.Range(-canvasRange, canvasRange), Random.Range(-canvasRange, canvasRange));
            image.rectTransform.sizeDelta = new Vector2(Random.Range(50, 100), Random.Range(50, 100));
            image.sprite = sprites[Random.Range(0, sprites.Length)];

            for (float i = 0; i < 1; i += Time.deltaTime)
            {
                image.color = new Color(1, 1, 1, 0 + i);
                yield return null;
            }
            
        }
    }
}
