using UnityEngine;
using System.Collections.Generic;

public class CursorController : MonoBehaviour
{
    public GameObject selectedObject;
    public List<GameObject> snapToObjects = new();
    public float yOffset;
    public List<Vector3> snapPoints = new();
    public List<BinPanelController> binPanels = new(); // Changed to List<BinPanelController>

    public static CursorController instance;

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

    void Start()
    {
        snapPoints = new List<Vector3>();

        foreach (GameObject snapToObject in snapToObjects)
        {
            snapPoints.Add(snapToObject.transform.position);
        }
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 closestSnapPoint = snapPoints[0];
        int snapIndex = 0;
        float closestDistance = Vector3.Distance(cursorPosition, closestSnapPoint);
        foreach (Vector3 snapPoint in snapPoints)
        {
            float distance = Vector3.Distance(cursorPosition, snapPoint);
            if (distance < closestDistance)
            {
                closestSnapPoint = snapPoint;
                snapIndex = snapPoints.IndexOf(snapPoint);
            }
        }

        closestSnapPoint.y += yOffset;

        if (selectedObject != null)
        {
            selectedObject.transform.position = closestSnapPoint;
            ActivatePanel(snapIndex);
        }
    }

    public void PickUpObject(GameObject obj)
    {
        selectedObject = obj;
    }

    public void DropObject()
    {
        selectedObject = null;
        for (int i = 0; i < binPanels.Count; i++) // Changed to binPanels.Count
        {
            if (binPanels[i].IsOn)
            {
                binPanels[i].TurnOffPanel();
            }
        }
    }

    private void ActivatePanel(int index)
    {
        for (int i = 0; i < binPanels.Count; i++) // Changed to binPanels.Count
        {
            if (i == index && !binPanels[i].IsOn)
            {
                binPanels[i].TurnOnPanel();
            }
            else if (i != index && binPanels[i].IsOn)
            {
                binPanels[i].TurnOffPanel();
            }
        }
    }
}