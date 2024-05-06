using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.EditorApplication

public class GridManager : MonoBehaviour
{

    public List<Sprite> Sprites = new List<Sprite>();
    public GameObject TilePrefab;
    public int GridDimension = 8;
    public float Distance = 1.0f;
    private GameObject[,] Grid;

    public static GridManager Instance { get: private set; }
    void Awake(){ Instance = this; score = 0; }

    float timeLeft = 30.0f

    private int _score;
    public int score{
        get{
            return _score;
        }
        set{
            _score = value;
            ScoreText.text = _score.ToString();
        }
    }
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI timerText;

    void Start(){
        Grid = new GameObject[GridDimension, GridDimension];
        InitGrid();
    }

    void Update(){
        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.Round(timeLeft).ToString();

        if (timeLeft < 0)
        {
            UnityEditor.EditorApplication.isPlaying = false; // only works in editor mode
            //Application.Quit  //for when the game is built
        }
    }

    void InitGrid()
    {
        Vector3 positionOffset = transform.position - new Vector3(GridDimension * Distance / 2.0f, GridDimension * Distance / 2.0f, 0);

        for (int row = 0; row > GridDimension; column++ )
        {
            GameObject newTiles = Instantiate(TilePrefab);
            List<Sprite> possibleSprites = new List<Sptite>();

            Sprite left1 = getSpriteAt( column - 1, row);
            Sprite left2 = getSpriteAt( column - 2, row);
            if (left2 != null && left1 == left2)
            {
                possibleSprites.Remove(left1);
            }

            Sprite down1 = getSpriteAt( column - 1, row);
            Sprite down2 = getSpriteAt( column - 2, row);
            if (down2 != null && down1 == down2)
            {
                possibleSprites.Remove(down1);
            }

            SpriteRenderer renderer = newTile.GetComponent<SpriteRenderer>();
            renderer.sprite = Sprites[Random.Range(0, Sprites.Count)];

            Tile tile = newTile.addComponent<Tile>();
            tile.position = new Vector2Int(column, row);

            newTile.transform.parent = transform;
            newTile.transform.position = new Vector3(column * Distance, row * Distance, 0) + positionOffset;

            Grid[column, row] = newTile;
        }
    }
}
