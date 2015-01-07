using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour 
{
    private static InGameUIManager instance = null;
    public static InGameUIManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public bool paused { get; set; }
    [SerializeField]
    private Canvas UICanvas;
    [SerializeField]
    private Text scoreText;
    private int score = 0;
    private int visibleScore = 0;
    private float scoreCountTimer = 0;
    [SerializeField]
    private float incrimentSpeed = 100.0f;
    [SerializeField]
    private Text timerText;
    private float time = 120.0f;
    [SerializeField]
    private Text shipsText;

    [SerializeField]
    private Canvas PausedCanvas;

    [SerializeField]
    private Canvas GameLostCanvas;

    [SerializeField]
    private Canvas GameWonCanvas;
    [SerializeField]
    private Text endScoreText;

	void Start () 
    {
        scoreText.text = "0";
        timerText.text = "02:00";
        shipsText.text = BoatManager.Instance.boatsLeft.ToString();
	}
	
	void Update () 
    {
        if (GameManager.WhatState() == "playing" && !paused)
        {
            shipsText.text = BoatManager.Instance.boatsLeft.ToString(); // update amount of ships
            scoreText.text = visibleScore.ToString(); // update score 
            LerpScore();
            Timer();
        }
	}

    public void AddScore(int amount)
    {
        score += amount;
    }

    private void LerpScore()
    {
        if (visibleScore < score)
        {
            scoreCountTimer += Time.deltaTime * incrimentSpeed;
            if (scoreCountTimer >= 1.0f)
            {
                visibleScore++;
                scoreCountTimer = 0;
            }
        }
    }

    private void Timer()
    {
        time -= Time.deltaTime;

        int temp = Mathf.FloorToInt(time);

        int tempMinute = Mathf.FloorToInt(time / 60);
        int tempSecond;
        if (time > 60)
        {
            tempSecond = Mathf.FloorToInt(time % (tempMinute * 60));
        }
        else if(time < 60)
        {
            tempSecond = Mathf.FloorToInt(time);
        }
        else
        {
            tempSecond = 00;
        }

        string minute;
        string second;

        if (tempMinute < 10)
        {
            minute = "0" + tempMinute;
        }
        else
        {
            minute = tempMinute.ToString();
        }

        if(tempSecond < 10)
        {
            second = "0" + tempSecond;
        }
        else
        {
            second = tempSecond.ToString();
        }

        timerText.text = minute + ":" + second;

        if(BoatManager.Instance.boatsLeft == 0)
        {
            GameManager.Instance.NewGameState(GameManager.Instance.stateGameWon);
            endScoreText.text = score.ToString();
            BoatManager.Instance.DisableBoats(false);
            UICanvas.gameObject.SetActive(false);
            GameWonCanvas.gameObject.SetActive(true);
            PlayerController.Instance.Reset();
        }

        if (time <= 0)
        {
            GameManager.Instance.NewGameState(GameManager.Instance.stateGameLost);
            BoatManager.Instance.DisableBoats(false);
            UICanvas.gameObject.SetActive(false);
            GameLostCanvas.gameObject.SetActive(true);
            PlayerController.Instance.Reset();
        }
    }

    public void Paused(bool isPaused)
    {
        if (isPaused)
        {
            paused = true;
            BoatManager.Instance.DisableBoats(false);
            UICanvas.gameObject.SetActive(false);
            PausedCanvas.gameObject.SetActive(true);
        }
        else
        {
            paused = false;
            BoatManager.Instance.DisableBoats(true);
            PausedCanvas.gameObject.SetActive(false);
            UICanvas.gameObject.SetActive(true);
        }
    }

    public void Reset()
    {
        BoatManager.Instance.Reset();
        time = 120.0f;
        score = 0;
        visibleScore = 0;
        GameLostCanvas.gameObject.SetActive(false);
        GameWonCanvas.gameObject.SetActive(false);
        UICanvas.gameObject.SetActive(true);
        GameManager.Instance.NewGameState(GameManager.Instance.stateGamePlaying);
    }

    public void Quit()
    {
        GameManager.Instance.NewGameState(GameManager.Instance.stateGameMenu);
        Application.LoadLevel("menu");
    }

    public void NextLevel()
    {
        BoatManager.Instance.NextLevel();
        time = 120.0f;
        score = 0;
        visibleScore = 0;
        GameWonCanvas.gameObject.SetActive(false);
        UICanvas.gameObject.SetActive(true);
        GameManager.Instance.NewGameState(GameManager.Instance.stateGamePlaying);
    }
}
