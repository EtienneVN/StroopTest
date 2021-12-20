using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    #region CONSTRUCTORS

    #endregion

    #region PRIVATE MEMBERS

    #endregion

    #region PUBLIC MEMBERS

    #endregion

    #region PROPERTIES

    #endregion

    #region MONOBEHAVIOUR

    private void onEnable() {

    }

    private void Awake() {

    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    #endregion

    #region FUNCTIONS

    public void StartButton() {
        GameManager._instance.transitionToState(GameManager.GameState.GAMEPLAY);
        // GameManager._instance.currentState = GameManager.GameState.GAMEPLAY;
        GameManager._instance.initPlayerData();
    }

    public void OptionButton() {
        GameManager._instance.transitionToState(GameManager.GameState.OPTIONS);
        // GameManager._instance.currentState = GameManager.GameState.OPTIONS;
    }

    public void QuitButton() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
         #endif
    }

    #endregion

}