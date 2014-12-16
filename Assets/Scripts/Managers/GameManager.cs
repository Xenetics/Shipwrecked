using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    private static int stateNumber;
	public GameState currentState;                      // State Numbers
    public StateGameIntro stateGameIntro { get; set; }  // 0
    public StateGameMenu stateGameMenu { get; set; }    // 1
    public StateCustomize stateCustomize { get; set; }  // 2
	public StateGamePlaying stateGamePlaying{get;set;}  // 3
	public StateGameWon stateGameWon{get;set;}          // 4
	public StateGameLost stateGameLost{get;set;}        // 5

    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }
	
	private void Awake () 
	{
        DontDestroyOnLoad(this.gameObject);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        stateGameIntro = new StateGameIntro(this);
        stateGameMenu = new StateGameMenu(this);
        stateCustomize = new StateCustomize(this);
		stateGamePlaying = new StateGamePlaying(this);
		stateGameWon = new StateGameWon(this);
		stateGameLost = new StateGameLost(this);
	}
	
	private void Start () 
	{
		NewGameState( stateGameIntro );
        stateNumber = 0;
	}
	
	private void Update () 
	{
		if (currentState != null)
		{
			currentState.StateUpdate();
		}
	}
	
	private void OnGUI () 
	{
		if (currentState != null)
		{
			currentState.StateGUI();
		}
	}
	
	public void NewGameState(GameState newState)
	{
		if( null != currentState)
		{
			currentState.OnStateExit();
		}
		currentState = newState;
		currentState.OnStateEntered();        
        
        if(currentState == stateGameIntro)
        {
            stateNumber = 0;
        }
        else if(currentState == stateGameMenu)
        {
            stateNumber = 1;
        }
        else if(currentState == stateCustomize)
        {
            stateNumber = 2;
        }
        else if(currentState == stateGamePlaying)
        {
            stateNumber = 3;
        }
        else if(currentState == stateGameWon)
        {
            stateNumber = 4;
        }
        else if (currentState == stateGameLost)
        {
            stateNumber = 5;
        }
	}

    public static bool IsPlaying()
    {
        if (GameManager.Instance.currentState == GameManager.Instance.stateGamePlaying)
        {
            return true;
        }
        return false;
    }

    public static bool IsMenu()
    {
        if (GameManager.Instance.currentState == GameManager.Instance.stateGameMenu)
        {
            return true;
        }
        return false;
    }

    public static string WhatState()
    {
        switch(stateNumber)
        {
            case 0:
                return "intro";
            case 1:
                return "menu";
            case 2:
                return "customize";
            case 3:
                return "playing";
            case 4:
                return "won";
            case 5:
                return "lost";
        }
        return " ";
    }
}