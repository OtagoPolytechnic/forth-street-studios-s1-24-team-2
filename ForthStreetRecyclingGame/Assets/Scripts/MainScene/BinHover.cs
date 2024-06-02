/*
 * File: Hoverable.cs
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

public class BinHover : MonoBehaviour
{
    private CursorController cursorController;
    [SerializeField] BinController binController;


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