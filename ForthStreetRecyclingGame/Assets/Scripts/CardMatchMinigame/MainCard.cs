using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    [SerializeField] private CardMatchManager controller;
    [SerializeField] private GameObject Card_Back;

    public void OnMouseDown()
    {
        if (Card_Back.activeSelf && controller.canReveal)
        {
            SFXManager.Instance.Play("DrawCard");
            Card_Back.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    private int _id;
    public int id
    {
        get { return _id; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void Unreveal()
    {
        Card_Back.SetActive(true);
    }
}

