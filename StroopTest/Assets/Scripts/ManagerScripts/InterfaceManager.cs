using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class is responsible for all of the button navigation of the project
/// </summary>
public class InterfaceManager : MonoBehaviour
{
    private static InterfaceManager _instance;

    InterfaceManager() {
        _instance = this;
    }

    #region MONOBEHAVIOUR

    private void Awake() {
        _instance = this;
        DontDestroyOnLoad(this);
    }

    #endregion

    /// <summary>
    /// Button event to start the game
    /// </summary>
    public void StartGame() {
        GameManager.Instance.TransitionToState(GameManager.GameState.Gameplay);
        GameManager.Instance.InitPlayerData();
    }

    /// <summary>
    /// Button event for exiting the game
    /// </summary>
    public void QuitProgram() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
         #endif
    }

    /// <summary>
    /// Button event to resume the game from the pause menu
    /// </summary>
    public void ResumeGame() {
        GameManager.Instance.pause.SetActive(false);
        GameManager.Instance.TransitionToState(GameManager.GameState.Gameplay);
    }

    /// <summary>
    /// Button event to transition to the options menu
    /// </summary>
    public void OptionMenu() {
        GameManager.Instance.TransitionToState(GameManager.GameState.Options);
    }

    /// <summary>
    /// Button event to transition to the pause menu
    /// </summary>
    public void PauseMenu() {
        GameManager.Instance.TransitionToState(GameManager.GameState.Pause);
    }
    
    /// <summary>
    /// Button event to transition to the title menu 
    /// </summary>
    public void TitleMenu() {
        GameManager.Instance.TransitionToState(GameManager.GameState.Title);
    }

    /// <summary>
    /// Button event to transition to the post game screen
    /// </summary>
    public void PostGame() {
        GameManager.Instance.TransitionToState(GameManager.GameState.PostGame);
    }

    /// <summary>
    /// Button event to return to the previous menu
    /// </summary>
    public void BackButton() {
        GameManager.Instance.TransitionToState(GameManager.Instance.previousState);
    }
}