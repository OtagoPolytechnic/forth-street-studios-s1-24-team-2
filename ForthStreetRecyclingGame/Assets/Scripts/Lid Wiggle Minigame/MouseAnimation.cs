using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseAnimation : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] Sprite centreMouse;
    [SerializeField] Sprite leftMouse;
    [SerializeField] Sprite rightMouse;
    private const int loops = 5;
    private const float transVal = 0.5f; // the transparency value
    private const float swapTime = 0.3f; // time it takes for the image to swap sprites
    private const float transTime = 0.1f; // time it takes for the image to lower or raise transparency value
    private const float timeBeforeFade = 1.5f; // time before the image fades out
    private const float imgColour = 0.9f; // the colour of the image

    void Start()
    {
        if (LidWiggleManager.instance != null) { return; }
        StartCoroutine(FadeIn());
        StartCoroutine(SpriteSwap());
        StartCoroutine(FadeOut());
    }

    public void PlayTutorialAnimation()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(SpriteSwap());
        StartCoroutine(FadeOut());
    }

    //Fades the image in
    IEnumerator FadeIn()
    {
        for (float i = 0; i < transVal; i += Time.deltaTime)
        {
            GetComponent<Image>().color = new Color(imgColour, imgColour, imgColour, i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    //Moves the image left and right
    IEnumerator SpriteSwap()
    {
        for (int i = 0; i < loops; i++) //loops 5 times
        {
            yield return new WaitForSeconds(swapTime);
            GetComponent<Image>().sprite = leftMouse; // swaps the left and right mouse images
            yield return new WaitForSeconds(swapTime);
            GetComponent<Image>().sprite = rightMouse;
        }
    }

    //Fades the image out
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1.5f); // wait time before starting to fade so the animation stays on screen for a bit
        for (float i = transVal; i > 0f; i -= Time.deltaTime) // lowers transparency
        {
            GetComponent<Image>().color = new Color(imgColour, imgColour, imgColour, i); 
            yield return new WaitForSeconds(0.01f);
        }
    }
}
