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
    private int bonus = 0;
    private float scoreCountTimer = 0;
    [SerializeField]
    private float incrimentSpeed = 100.0f;
    [SerializeField]
    private int bonusPerSecond = 5;
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
    [SerializeField]
    private Text highScore;

    [SerializeField]
    private Canvas InstructionCanvas;
    [SerializeField]
    private Image InstructionImage;
    private bool tutDone = false;

	void Start () 
    {
        scoreText.text = "0";
        timerText.text = "02:00";
        shipsText.text = BoatManager.Instance.boatsLeft.ToString();
	}
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Paused(false);
            }
            else
            {
                Paused(true);
            }
        }
        if (!paused)
        {
            if (LevelManager.Instance.levelChosen == 1 && BoatManager.Instance.boatsLeft != 0 && time > 0 && tutDone == false)
            {
                if (time < 115)
                {
                    InstructionImage.color = new Color(InstructionImage.color.r, InstructionImage.color.g, InstructionImage.color.b, InstructionImage.color.a - Time.deltaTime);
                }
                InstructionCanvas.gameObject.SetActive(true);
                if (InstructionImage.color.a < 0)
                {
                    tutDone = true;
                }
            }
            else
            {
                InstructionImage.color = new Color(InstructionImage.color.r, InstructionImage.color.g, InstructionImage.color.b, 255);
                InstructionCanvas.gameObject.SetActive(false);

            }

            if (GameManager.WhatState() == "playing")
            {
                shipsText.text = BoatManager.Instance.boatsLeft.ToString(); // update amount of ships
                scoreText.text = visibleScore.ToString(); // update score 
                LerpScore();
                Timer();
            }
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
            visibleScore = score; // temp since the text system is junk
            //visibleScore = Mathf.FloorToInt(Mathf.Lerp(visibleScore, score, Time.deltaTime * incrimentSpeed)); // did not work as expected--------------------------------------------
            /*
            scoreCountTimer += Time.deltaTime * incrimentSpeed;
            if (scoreCountTimer >= 1.0f)
            {
                visibleScore++;
                scoreCountTimer = 0;
            }
            */
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
            TallyScore();
            SaveScore();
            InstructionCanvas.gameObject.SetActive(false);
            endScoreText.text = score.ToString();
            highScore.text = PlayerPrefs.GetInt("lvl" + LevelManager.Instance.levelChosen).ToString();
            BoatManager.Instance.DisableBoats(false);
            CreatureManager.Instance.DisableCreatures(false);
            UICanvas.gameObject.SetActive(false);
            GameWonCanvas.gameObject.SetActive(true);
            PlayerController.Instance.Reset();
        }

        if (time <= 0)
        {
            GameManager.Instance.NewGameState(GameManager.Instance.stateGameLost);
            InstructionCanvas.gameObject.SetActive(false);
            BoatManager.Instance.DisableBoats(false);
            CreatureManager.Instance.DisableCreatures(false);
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
            CreatureManager.Instance.DisableCreatures(false);
            UICanvas.gameObject.SetActive(false);
            PausedCanvas.gameObject.SetActive(true);
        }
        else
        {
            paused = false;
            BoatManager.Instance.DisableBoats(true);
            CreatureManager.Instance.DisableCreatures(true);
            PausedCanvas.gameObject.SetActive(false);
            UICanvas.gameObject.SetActive(true);
        }
    }

    public void Reset()
    {
        AudioManager.Instance.PlaySound("button");
        Paused(false);
        BoatManager.Instance.Reset();
        CreatureManager.Instance.Reset();
        time = 120.0f;
        score = 0;
        visibleScore = 0;
        bonus = 0;
        GameLostCanvas.gameObject.SetActive(false);
        GameWonCanvas.gameObject.SetActive(false);
        UICanvas.gameObject.SetActive(true);
        GameManager.Instance.NewGameState(GameManager.Instance.stateGamePlaying);
    }

    public void Quit()
    {
        AudioManager.Instance.PlaySound("button");
        Paused(false);
        GameManager.Instance.NewGameState(GameManager.Instance.stateGameMenu);
        Application.LoadLevel("menu");
    }

    public void NextLevel()
    {
        AudioManager.Instance.PlaySound("button");
        Paused(false);
        if (LevelManager.Instance.levelChosen < LevelManager.Instance.totalLevels)
        {
            LevelManager.Instance.levelChosen++;
            LevelManager.Instance.SaveLevelProgress(LevelManager.Instance.levelChosen);
        }
        BoatManager.Instance.NextLevel();
        CreatureManager.Instance.NextLevel();
        time = 120.0f;
        score = 0;
        visibleScore = 0;
        highScore.text = "0";
        GameWonCanvas.gameObject.SetActive(false);
        UICanvas.gameObject.SetActive(true);

        GameManager.Instance.NewGameState(GameManager.Instance.stateGamePlaying);
    }

    private void TallyScore()
    {
        bonus = Mathf.FloorToInt(time) * bonusPerSecond * CreatureManager.Instance.creaturesAlive;
        score += bonus;
    }

    private void SaveScore()
    {
        if (score > PlayerPrefs.GetInt("lvl" + LevelManager.Instance.levelChosen))
        {
            PlayerPrefs.SetInt("lvl" + LevelManager.Instance.levelChosen, score);
        }
    }
}
