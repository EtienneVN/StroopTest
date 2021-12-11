using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region CONSTRUCTORS

    #endregion

    #region PRIVATE MEMBERS

    private int stateNum; 
    #endregion

    #region PUBLIC MEMBERS

    public static GameManager Instance;

    [Header("SCREEN OBJECTS")]
    public GameObject TitleScreen;
    public GameObject GamePlay;
    public GameObject Options;
    public GameObject Pause;
    public GameObject PostGame;

    [Space(10)]
    [Header("GAME STATE")]
    public GameState currentState = GameState.TITLE;
    public GameState previousState;
    public enum GameState
    {
        TITLE = 0,
        GAMEPLAY = 1,
        OPTIONS = 2,
        PAUSE = 3,
        PostGame = 4
    }

    #endregion

    #region MONOBEHAVIOUR

    private void Awake() {
        if ( !Instance ) Instance = this;
        _init();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        switch (currentState) {

            case GameState.TITLE:
                previousState = currentState;
                ClearScreen();
                TitleScreen.SetActive(true);
                break;
            case GameState.OPTIONS:
                previousState = currentState;
                ClearScreen();
                Options.SetActive(true);
                break;
            case GameState.GAMEPLAY:
                ClearScreen();
                GamePlay.SetActive(true);
                break;
            case GameState.PAUSE:
                ClearScreen();
                Pause.SetActive(true);
                break;
            case GameState.PostGame:
                ClearScreen();
                PostGame.SetActive(true);
                break;
            default:
                currentState = GameState.TITLE;
                _init();
                break;
        }

    }

    #endregion

    #region FUNCTIONS

    [Button("Game init")]
    private void _init() {
        TitleScreen.SetActive(true);
        GamePlay.SetActive(false);
        Options.SetActive(false);
        Pause.SetActive(false);
        PostGame.SetActive(false);
    }

    /// <summary>
    /// A button created to test all of the scenes
    /// </summary>
    [Button("nextState")]
    private void NextState() {
        stateNum++;

        if ( stateNum >= 5 ) {
            stateNum = 0;
        }

        previousState = currentState;
        currentState = (GameState)stateNum;
    }

    private void ClearScreen() {
        TitleScreen.SetActive(false);
        GamePlay.SetActive(false);
        Options.SetActive(false);
        Pause.SetActive(false);
        PostGame.SetActive(false);
    }

    #endregion

}