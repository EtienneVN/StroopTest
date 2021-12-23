using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InterfaceManager:
/// this class is responsible for all of the navigation buttons,
///  
/// </summary>
public class InterfaceManager : MonoBehaviour
{

    public void StartGame() {
        GameManager._instance.transitionToState(GameManager.GameState.GAMEPLAY);
        GameManager._instance.initPlayerData();
    }

    public void QuitProgram() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
         #endif
    }
    
    public void resumeGame() {
        GameManager._instance.Pause.SetActive(false);
        GameManager._instance.transitionToState(GameManager.GameState.GAMEPLAY);
    }
    
    public void OptionMenu() {
        GameManager._instance.transitionToState(GameManager.GameState.OPTIONS);
    }

    public void pauseMenu() {
        GameManager._instance.transitionToState(GameManager.GameState.PAUSE);
    }

    public void TitleMenu() {
        GameManager._instance.transitionToState(GameManager.GameState.TITLE);
    }

    public void PostGame() {
        GameManager._instance.transitionToState(GameManager.GameState.PostGame);
    }

    public void BackButton() {
        GameManager._instance.transitionToState(GameManager._instance.previousState);
    }
}