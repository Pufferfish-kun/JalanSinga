using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int levelToUnlock = 3;
    public Scenefader fader;
    public bool endGame = false;

    public const int gridRows = 3;
    public const int gridCols = 7;
    public const float offsetX = 100f;
    public const float offsetY = 100f;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;

    private void Start()
    {
        Vector3 startPos = originalCard.transform.position; //the position of the first card. All other cards are offset from here.
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11};
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard Card;
                if (i == 0 && j == 0)
                {
                    Card = originalCard;
                }
                else
                {
                    Card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                Card.ChangeSprite(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                Card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i< newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    //

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _score = 0;
    [SerializeField] private TextMesh scoreLabel;

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }else if (_secondRevealed == null)
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            _score += 10;
            scoreLabel.text = "Score:" + _score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

    public void WinLevel()
    {
        Debug.Log("LEVEL ENDED!");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        endGame = true;
    }

    void Update()
    {
        if (endGame==true && Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
        //fader.FadeTo("LevelSelect");
    }


}
