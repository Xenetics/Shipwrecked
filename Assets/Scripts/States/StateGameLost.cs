﻿using UnityEngine;
using System.Collections;
public class StateGameLost : GameState 
{
	public StateGameLost(GameManager manager):base(manager){ }

    private float time;
    private bool laughPlaying = false;

	public override void OnStateEntered()
    {

    }
	public override void OnStateExit()
    {

    }
	public override void StateUpdate() {}
	public override void StateGUI() 
	{
        time += Time.deltaTime;
        if (!laughPlaying && time > 0.1)
        {
            AudioManager.Instance.PlaySound("lostSound");
            laughPlaying = true;
        }
        if (time >= 8)
        {
            AudioManager.Instance.leaveState = true;
            gameManager.NewGameState(gameManager.stateGameMenu);
            Application.LoadLevel("Menu");
        }
	}
}
