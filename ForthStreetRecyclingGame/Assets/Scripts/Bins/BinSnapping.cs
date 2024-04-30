using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinSnapping : MonoBehaviour
{
    private GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        arrow = transform.GetChild(0).gameObject;
        arrow.SetActive(false);
    }

    void OnMouseEnter()
    {
        arrow.SetActive(true);
    }

    void OnMouseExit()
    {
        arrow.SetActive(false);
    }
}
