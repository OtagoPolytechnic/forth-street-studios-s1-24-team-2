/*
 * File: BinHover.cs
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// Controls the hover effect for the waste bins in the main scene
/// </summary>
public class BinHover : MonoBehaviour
{
    [SerializeField] BinController binController;

    private CursorController cursorController;

    void Start()
    {
        cursorController = CursorController.Instance;
    }

    void OnMouseEnter()
    {
        if (cursorController.IsHoldingObject == false) { return; }
        cursorController.SelectBin(binController);
    }
}