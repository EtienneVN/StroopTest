using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region CONSTRUCTORS
    public static GameManager Instance;
    #endregion

    #region STRUCTS

    public struct Player
    {
        public int health;
        public int score;
        public float TotalTime;
    }

    #endregion

    #region ENUMS

    public enum GameState
    {
        TITLE = 0,
        OPTIONS = 1,
        GAMEPLAY = 2,
        PAUSE = 3,
        PostGame = 4
    }

    #endregion

    #region PRIVATE MEMBERS

    private int stateNum;

    #endregion

    #region PUBLIC MEMBERS

    [Space]
    [Header("PLAYER OBJECT")]
    public Player player;

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
                player.TotalTime += Time.deltaTime;
                break;
            case GameState.PAUSE:
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

        if ( currentState == GameState.PAUSE ) {
            GamePlay.SetActive(true);
            
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

        transitionToState((GameState)stateNum);
    }

    public void transitionToState(GameState state) {
        previousState = currentState;
        currentState = state;
    }
    
    public void initPlayerData() {
        Instance.player.health = 25;
        Instance.player.score = 0;
        Instance.player.TotalTime = 0;
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