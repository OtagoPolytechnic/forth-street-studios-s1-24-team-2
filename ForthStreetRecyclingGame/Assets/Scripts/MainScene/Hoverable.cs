using UnityEngine;

public class Hoverable : MonoBehaviour
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