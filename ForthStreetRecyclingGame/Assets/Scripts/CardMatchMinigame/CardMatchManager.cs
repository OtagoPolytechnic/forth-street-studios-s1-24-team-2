using System.Collections;
using UnityEngine;
using TMPro;

public class CardMatchManager : Minigame
{
    public const int gridRows = 2;
    public const int gridCols = 5;
    public const float offsetX = 4f;
    public const float offsetY = 5f;

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _score = 0;
    public TextMeshPro scoreLabel;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;


    private void Start()
    {
        Deal();
    }

    private void Deal()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                    card.transform.SetParent(transform);                 
                }

                card.Unreveal();

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private void DestroyCards()
    {
        // Destroy all cards except the original
        foreach (MainCard card in FindObjectsOfType<MainCard>())
        {
            if (card != originalCard)
            {
                Destroy(card.gameObject);
            }
        }
    }
    private void Update()
    {
        if (!success && _score == gridCols * gridRows / 2)
        {
            success = true;

            InvokeGameOver();
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {
        SFXManager.Instance.Play("DrawCard");
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            SFXManager.Instance.Play("Correct");
            _score++;
            scoreLabel.text = "Score: " + _score;
        }
        else
        {          
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            yield return new WaitForSeconds(0.05f);
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;

    }

    public override void Reset()
    {
        success = false;
        DestroyCards();
        Deal();
        _score = 0;
        scoreLabel.text = "Score: " + _score;
    }

    public override void MinigameBegin()
    {
        base.MinigameBegin();
        SFXManager.Instance.Play("DrawHand");
    }
}
