using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//using UnityEditor.EditorApplication;

public class GridManager : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>(); // List of sprites to be used for tiles
    public GameObject TilePrefab; // Prefab for each tile
    public int GridDimension = 8; // Dimension of the grid
    public float Distance = 1.0f; // Distance between each block
    private GameObject[,] Grid; // 2D array to hold the grid of tiles
    public static GridManager Instance { get; private set; } // Singleton instance of GridManager

    void Awake() { Instance = this; Score = 0; } // Initializing singleton instance

    float timeLeft = 30.0f; // Timer for the game

    private int _score; // Score tracking
    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            ScoreText.text = _score.ToString();
        }
    }
    public TextMeshProUGUI ScoreText; // Text for displaying score
    public TextMeshProUGUI timerText; // Text for displaying timer

    void Start() // Initialize the grid
    {
        // Create the grid
        Grid = new GameObject[GridDimension, GridDimension];
        InitGrid();
    }

    void Update() // Update is called once per frame
    {
        // Update the timer
        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.Round(timeLeft).ToString();
        if (timeLeft < 0) // End the game when time runs out
        {
            UnityEditor.EditorApplication.isPlaying = false; //only works in unity editor
                                                             //to make it work in the actual game not in editor it needs to be changed 
                                                             //Application.Quit  //for the main game  
        }
    }

    void InitGrid() // Create the grid of tiles
    {
        // Calculate position offset for grid alignment
        Vector3 positionOffset = transform.position - new Vector3(GridDimension * Distance / 2.0f, GridDimension * Distance / 2.0f, 0);

        // Loop through each row and column to create tiles
        for (int row = 0; row < GridDimension; row++)
            for (int column = 0; column < GridDimension; column++)
            {
                GameObject newTile = Instantiate(TilePrefab); // Create a new tile object

                List<Sprite> possibleSprites = new List<Sprite>(); // Get possible sprites for the tile

                // Check for potential matches in adjacent tiles
                Sprite left1 = GetSpriteAt(column - 1, row);
                Sprite left2 = GetSpriteAt(column - 2, row);
                if (left2 != null && left1 == left2)
                {
                    possibleSprites.Remove(left1);
                }

                Sprite down1 = GetSpriteAt(column, row - 1);
                Sprite down2 = GetSpriteAt(column, row - 2);
                if (down2 != null && down1 == down2)
                {
                    possibleSprites.Remove(down1);
                }

                // Set random sprite for the tile
                SpriteRenderer renderer = newTile.GetComponent<SpriteRenderer>();
                renderer.sprite = Sprites[Random.Range(0, Sprites.Count)];

                // Add Tile component to the tile object
                Tile tile = newTile.AddComponent<Tile>();
                tile.Position = new Vector2Int(column, row);

                // Set parent and position for the tile
                newTile.transform.parent = transform;
                newTile.transform.position = new Vector3(column * Distance, row * Distance, 0) + positionOffset;

                // Store the tile in the grid array
                Grid[column, row] = newTile;
            }
    }

    public void SwapTiles(Vector2Int tile1Position, Vector2Int tile2Position)
    {
        // Get the GameObjects and their SpriteRenderers for the tiles
        GameObject tile1 = Grid[tile1Position.x, tile1Position.y];
        SpriteRenderer renderer1 = tile1.GetComponent<SpriteRenderer>();

        GameObject tile2 = Grid[tile2Position.x, tile2Position.y];
        SpriteRenderer renderer2 = tile2.GetComponent<SpriteRenderer>();

        // Swap the sprites between the tiles
        Sprite temp = renderer1.sprite;
        renderer1.sprite = renderer2.sprite;
        renderer2.sprite = temp;

        bool changesOccurs = CheckMatches(); // Check for matches after the swap
        if (!changesOccurs) // If no matches found, revert the swap
        {
            temp = renderer1.sprite;
            renderer1.sprite = renderer2.sprite;
            renderer2.sprite = temp;
        }
        else
        { // If matches found, fill empty spaces and continue checking for matches
            do
            {
                FillBlocks();
            } while (CheckMatches());
        }
    }

    bool CheckMatches()
    {
        // HashSet to store matched tiles
        HashSet<SpriteRenderer> matchedTiles = new HashSet<SpriteRenderer>();
        for (int row = 0; row < GridDimension; row++) // Loop through each tile to check for matches
        {
            for (int column = 0; column < GridDimension; column++)
            {
                SpriteRenderer current = GetSpriteRendererAt(column, row); // Get the current tile's SpriteRenderer

                // Check for horizontal matches
                List<SpriteRenderer> horizontalMatches = FindColumnMatchForTile(column, row, current.sprite);
                if (horizontalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(horizontalMatches);
                    matchedTiles.Add(current);
                }

                List<SpriteRenderer> verticalMatches = FindRowMatchForTile(column, row, current.sprite);
                if (verticalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(verticalMatches);
                    matchedTiles.Add(current);
                }
            }
        }

        // Remove matched tiles and update score
        foreach (SpriteRenderer renderer in matchedTiles)
        {
            renderer.sprite = null;
        }
        Score += matchedTiles.Count;
        return matchedTiles.Count > 0; // Return true if matches found, false otherwise
    }

    // Find matching tiles in the same column
    List<SpriteRenderer> FindColumnMatchForTile(int col, int row, Sprite sprite)
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>(); // List to store matching tiles
        for (int i = col + 1; i < GridDimension; i++)
        {
            SpriteRenderer nextColumn = GetSpriteRendererAt(i, row);
            if (nextColumn.sprite != sprite)
            {
                break;
            }
            result.Add(nextColumn);
        }
        return result;
    }

    // Find matching tiles in the same row
    List<SpriteRenderer> FindRowMatchForTile(int col, int row, Sprite sprite)
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        for (int i = row + 1; i < GridDimension; i++)
        {
            SpriteRenderer nextRow = GetSpriteRendererAt(col, i);
            if (nextRow.sprite != sprite)
            {
                break;
            }
            result.Add(nextRow);
        }
        return result;
    }

    void FillBlocks() // Fill empty spaces with new tiles
    {
        for (int column = 0; column < GridDimension; column++) // Loop through each column and row to fill empty spaces
            for (int row = 0; row < GridDimension; row++)
            {
                while (GetSpriteRendererAt(column, row).sprite == null)
                {
                    SpriteRenderer current = GetSpriteRendererAt(column, row);
                    SpriteRenderer next = current;
                    for (int filler = row; filler < GridDimension - 1; filler++)
                    {
                        next = GetSpriteRendererAt(column, filler + 1);
                        current.sprite = next.sprite;
                        current = next;
                    }
                    next.sprite = Sprites[Random.Range(0, Sprites.Count)];
                }
            }
    }

    Sprite GetSpriteAt(int column, int row)
    {
        if (column < 0 || column >= GridDimension
            || row < 0 || row >= GridDimension)
            return null;
        GameObject tile = Grid[column, row];
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
        return renderer.sprite;
    }

    SpriteRenderer GetSpriteRendererAt(int column, int row)
    {
        if (column < 0 || column >= GridDimension
             || row < 0 || row >= GridDimension)
            return null;
        GameObject tile = Grid[column, row];
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
        return renderer;
    }
}
