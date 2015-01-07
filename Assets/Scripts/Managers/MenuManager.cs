using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

// Used for all Main menu functionality and interaction with the game manager

public class MenuManager : MonoBehaviour 
{
    [SerializeField]
    private GameObject transitionMonster;
    [SerializeField]
    private float transitionSpeed = 10.0f;
    [SerializeField]
    private float transitionTimer = 2.0f;
    private float transitionTimerActual;
    private bool transitioning = false;
    [SerializeField]
    public ButtonBehaviour playButton;
    [SerializeField]
    private Canvas menuCanvas;
    [SerializeField]
    private Canvas levelsCanvas;
    private Button[] lvlButtons;
    [SerializeField]
    private Canvas creditsCanvas;
    [SerializeField]
    private Canvas optionsCanvas;

    private Canvas currentCanvas;
    private bool canvasSwapped = false;

    private string buttonPushed;

	void Start () 
    {
        currentCanvas = menuCanvas;
        transitionTimerActual = transitionTimer;
	}

	void Update () 
    {
	    if(transitioning)
        {
            Vector3 temp = transitionMonster.transform.position;
            temp.y += Time.deltaTime * transitionSpeed;
            transitionMonster.transform.position = temp;
            transitionTimerActual -= Time.deltaTime;

            if (transitionTimerActual <= 1 && canvasSwapped == false)
            {
                Transition(buttonPushed);
            }

            if (transitionTimerActual <= 0)
            {
                Vector3 moveBack = new Vector3(temp.x, -20.0f, temp.z);
                transitionMonster.transform.position = moveBack;
                transitioning = false;
                transitionTimerActual = transitionTimer;
                canvasSwapped = false;
            }
        }

	}

    private void Transition(string button)  // swap enabled canvases
    {
        if (button == "play")
        {
            menuCanvas.gameObject.SetActive(false);
            levelsCanvas.gameObject.SetActive(true);
            canvasSwapped = true;
            if (lvlButtons == null)
            {
                GetLvlButtons();
            }
        }
        else if (button == "credits")
        {
            menuCanvas.gameObject.SetActive(false);
            creditsCanvas.gameObject.SetActive(true);
            canvasSwapped = true;
        }
        else if (button == "options")
        {
            menuCanvas.gameObject.SetActive(false);
            optionsCanvas.gameObject.SetActive(true);
            canvasSwapped = true;
        }
        else if (button == "back")
        {
            currentCanvas.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
                canvasSwapped = true;
        }
        else if(button == "lvl")
        {
            GameManager.Instance.NewGameState(GameManager.Instance.stateGamePlaying);
            Application.LoadLevel("game");
        }
    }

    public void PlayButton()
    {
        buttonPushed = "play";
        currentCanvas = levelsCanvas;
        transitioning = true;
    }

    public void CreditButton()
    {
        buttonPushed = "credits";
        currentCanvas = creditsCanvas;
        transitioning = true;
    }

    public void OptionsButton()
    {
        buttonPushed = "options";
        currentCanvas = optionsCanvas;
        transitioning = true;
    }

    public void BackButton()
    {
        buttonPushed = "back";
        currentCanvas = menuCanvas;
        transitioning = true;
    }

    public void LevelButton(int btn)
    {
        for (int i = 1; i <= lvlButtons.Length; ++i)
        {
            if(i == btn)
            {
                buttonPushed = "lvl";
                LevelManager.Instance.levelChosen = i;
                transitioning = true;
            }
        }
    }

    void GetLvlButtons()
    {
        Button[] potentialBtns = levelsCanvas.GetComponentsInChildren<Button>();

        lvlButtons = new Button[(potentialBtns.Length)];

        for (int i = 0; i < potentialBtns.Length; ++i)
        {
            if (potentialBtns[i].name != "Text")
            {
                lvlButtons[i] = potentialBtns[i];
            }
        }
    }
}
