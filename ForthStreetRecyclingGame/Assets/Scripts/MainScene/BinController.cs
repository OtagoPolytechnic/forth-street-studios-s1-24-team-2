/*
 * File: BinController.cs
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// Controls a waste bin in the main scene
/// </summary>
public class BinController : MonoBehaviour
{   
    [SerializeField] private GameObject binPanel;
    [SerializeField] private Transform dropPoint;

    private CursorController cursorController;

    public Vector3 DropPoint => dropPoint.position;

    void Start()
    {
        cursorController = CursorController.Instance;
        binPanel.SetActive(false);
    }

    public void ActivatePanel(bool active)
    {
        binPanel.SetActive(active);
    }
}