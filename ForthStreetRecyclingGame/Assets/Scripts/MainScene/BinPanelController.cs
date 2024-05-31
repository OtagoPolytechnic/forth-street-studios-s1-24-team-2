using UnityEngine;
using UnityEngine.UI;

public class BinPanelController : MonoBehaviour
{
    private Image image;
    [SerializeField] private GameObject binText;
    public bool IsOn { get; private set; }

    void Start()
    {
        // log this
        Debug.Log("BinPanelController Start");
        image = gameObject.GetComponent<Image>();
        if (image != null)
        {
            TurnOffPanel();
        }
        else
        {
            Debug.LogError("No Image component found on this GameObject.");
        }
    }

    public void TurnOffPanel()
    {
        if (image != null)
        {
            image.enabled = false;
        }

        binText?.SetActive(false);
        IsOn = false;
    }

    public void TurnOnPanel()
    {
        if (image != null)
        {
            image.enabled = true;
        }

        binText?.SetActive(true);
        IsOn = true;
    }
}