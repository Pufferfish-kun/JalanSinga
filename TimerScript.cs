using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 5f;
    float timeLeft;
    public GameObject gameOver;
    bool continueGame = true;

    public SceneController sceneController; 

    //Use this for initialization
    void Start()
    {
        gameOver.SetActive(false);
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    //Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            gameOver.SetActive(true);
            Time.timeScale = 0;
            continueGame = false;
            sceneController.WinLevel();
        }
    }

    public bool stopGame
    {
        get { return continueGame; }
    }
}
