/*
 * File: BinTrigger.cs
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;
using System.Collections.Generic;

public class CursorController : MonoBehaviour
{
    [SerializeField] private List<BinController> binControllers = new();
    [SerializeField] private MainGameManager mainGameManager;
    [field: SerializeField] public float ScaleIncrease { get; private set; } = 4;
    private GameObject selectedObject;

    public bool IsHoldingObject => selectedObject != null;

    #region Singleton
    public static CursorController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    /// <summary>
    /// Pick up an object
    /// This is called when the player clicks on a pickupable object
    /// </summary>
    public void PickUpObject(GameObject obj)
    {
        // Set the selected object to the object that was clicked
        selectedObject = obj;
        
        // Move the object to the closest bin
        BinController closestBin = GetClosetBin(obj.transform.position.x);
        SelectBin(closestBin);
    }

    public void DropObject() 
    {
        selectedObject = null;
        mainGameManager.BlockInput(true);
    }

    /// <summary>
    /// Select a bin
    /// This is called when the player hovers over a bin
    /// </summary>
    /// <param name="bin">The bin that was hovered over</param>
    public void SelectBin(BinController bin)
    {
        MoveHeldObject(bin.DropPoint);

        // Activate panel for current bin, deactivate others
        foreach (BinController binController in binControllers)
        {
            binController.ActivatePanel(binController == bin);
        }
    }

    /// <summary>
    /// Move the held object to a new position
    /// </summary>
    public void MoveHeldObject(Vector3 position) => selectedObject.transform.position = position;

    /// <summary>
    /// Get the closest bin to a given x position
    /// </summary>
    public BinController GetClosetBin(float xPosition)
    {
        BinController closestBin = binControllers[0];
        float closestDistance = Mathf.Abs(binControllers[0].transform.position.x - xPosition);
        foreach (BinController binController in binControllers)
        {
            float distance = Mathf.Abs(binController.transform.position.x - xPosition);
            if (distance < closestDistance)
            {
                closestBin = binController;
                closestDistance = distance;
            }
        }
        return closestBin;
    }

}