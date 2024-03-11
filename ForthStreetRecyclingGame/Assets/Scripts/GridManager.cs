using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>(); // Creates spirites list
    public GameObject TilePrefab; // Assigns the tile prefaps
    public int GridDimension = 10; //Generates the grid dimensions
    public float Distance = 1.0f; //Distance between blocks
    
    void Start()
    {
        Grid = new GameObject[GridDimension, GridDimension];
        InitGrid(); //Calls InitGrid function
    }

    void Update()
    {
        
    }

    void InitGrid () // Generate the grid
    {

    }
}
