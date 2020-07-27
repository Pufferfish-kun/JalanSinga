using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class NewQuizManager : MonoBehaviour
{
    public NewQuestion[] questions;
    private static List<NewQuestion> unansweredQuestions;
    private NewQuestion currQuestion;
    private static int gameScore;
    private static int questionLimit = 5;
    private static int answeredQuestions;

    [SerializeField] private Text questionText;
    [SerializeField] private Text[] optionsText;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Animator animator;
    [SerializeField] private float timeBetweenQuestions = 1f;

    [SerializeField] private Text AnswerAText;
    [SerializeField] private Text AnswerBText;
    [SerializeField] private Text AnswerCText;
    [SerializeField] private Text AnswerDText;

    public GameObject endGame;
    bool end;

    int levelToUnlock = 4;

    void Start()
    {
        endGame.SetActive(false);
        end = false;
        //Load all the qns into unansweredQns when starting the game
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<NewQuestion>();
            gameScore = 0;
            answeredQuestions = 0;
        }

        ScoreText.text = "Score:" + gameScore;

        SetCurrentQuestion();
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currQuestion = unansweredQuestions[randomQuestionIndex];

        questionText.text = currQuestion.question;
        for (int i = 0; i < 4; i++)
        {
            optionsText[i].text = currQuestion.options[i];
        }

        //Animation
        if (currQuestion.correctAns == "0")
        {
            AnswerAText.text = "CORRECT";
            AnswerBText.text = "WRONG";
            AnswerCText.text = "WRONG";
            AnswerDText.text = "WRONG";
        }
        else if (currQuestion.correctAns == "1")
        {
            AnswerAText.text = "WRONG";
            AnswerBText.text = "CORRECT";
            AnswerCText.text = "WRONG";
            AnswerDText.text = "WRONG";
        }
        else if (currQuestion.correctAns == "2")
        {
            AnswerAText.text = "WRONG";
            AnswerBText.text = "WRONG";
            AnswerCText.text = "CORRECT";
            AnswerDText.text = "WRONG";
        }
        else
        {
            AnswerAText.text = "WRONG";
            AnswerBText.text = "WRONG";
            AnswerCText.text = "WRONG";
            AnswerDText.text = "CORRECT";
        }

    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        if (answeredQuestions == questionLimit)
        {
            endGame.SetActive(true);
            endLevel();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void UserSelectA()
    {
        animator.SetTrigger("A");
        if (currQuestion.correctAns == "0")
        {
            gameScore += 10;
            ScoreText.text = "Score:" + gameScore;
            Debug.Log("CORRECT!");
        }
        else
        {
            Debug.Log("WRONG!");
        }
        answeredQuestions++;
        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectB()
    {
        animator.SetTrigger("B");
        if (currQuestion.correctAns == "1")
        {
            gameScore += 10;
            ScoreText.text = "Score:" + gameScore;
            Debug.Log("CORRECT!");
        }
        else
        {
            Debug.Log("WRONG!");
        }
        answeredQuestions++;
        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectC()
    {
        animator.SetTrigger("C");
        if (currQuestion.correctAns == "2")
        {
            gameScore += 10;
            ScoreText.text = "Score:" + gameScore;
            Debug.Log("CORRECT!");
        }
        else
        {
            Debug.Log("WRONG!");
        }
        answeredQuestions++;
        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectD()
    {
        animator.SetTrigger("D");
        if (currQuestion.correctAns == "3")
        {
            gameScore += 10;
            ScoreText.text = "Score:" + gameScore;
            Debug.Log("CORRECT!");
        }
        else
        {
            Debug.Log("WRONG!");
        }
        answeredQuestions++;
        StartCoroutine(TransitionToNextQuestion());
    }

    ///////Transit to Level Select///////
    
    public void endLevel()
    {
        Debug.Log("LEVEL ENDED!");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        end = true;
    }

    void Update()
    {
        if (end == true && Input.GetMouseButtonDown(0))
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
