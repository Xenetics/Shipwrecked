using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateCustomize : GameState
{
    public StateCustomize(GameManager manager) : base(manager) { }

    public override void OnStateEntered()
    {
        
    }

    public override void OnStateExit() 
    {
        
    }

    public override void StateUpdate()
    {
        Screen.showCursor = true;
    }

    public override void StateGUI()
    {
        GUI.skin = GuiManager.GetSkin();

        GUILayout.BeginArea(new Rect(Screen.width * 0.1f - Screen.width * 0.05f, Screen.height - Screen.height * 0.10f, Screen.width * 0.1f, Screen.height * 0.5f));
            if (GUILayout.Button("Back", GUILayout.Width(Screen.width * 0.1f)))
            {
                AudioManager.Instance.PlaySound("button");
                gameManager.NewGameState(gameManager.stateGameMenu);
                Application.LoadLevel("menu");
            }
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width * 0.5f - Screen.width * 0.075f, Screen.height - Screen.height * 0.10f, Screen.width * 0.15f, Screen.height * 0.5f));
            if (GUILayout.Button("Start"))
            {
                AudioManager.Instance.PlaySound("button");
                gameManager.NewGameState(gameManager.stateGamePlaying);
                Application.LoadLevel("game");
            }
        GUILayout.EndArea();
    }
}
