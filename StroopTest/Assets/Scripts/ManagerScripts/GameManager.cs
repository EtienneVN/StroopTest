using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Responsible for the games state and player data
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region CONSTRUCTORS

    #endregion

    #region STRUCTS

    /// <summary>
    /// A data structure containing the data for the player 
    /// </summary>
    private struct Player
    {
        public int Health;
        public int Score;
        public float TotalTime;
    }

    #endregion

    #region ENUMS

    /// <summary>
    /// A set number of states that the game will transition through
    /// </summary>
    public enum GameState
    {
        Title = 0,
        Options = 1,
        Gameplay = 2,
        Pause = 3,
        PostGame = 4
    }

    #endregion

    #region PRIVATE MEMBERS

    private int _stateNum = 1;
    private const string TestString = "Test Buttons"; 

    #endregion

    #region PUBLIC MEMBERS

    [Space]
    [Header("PLAYER OBJECT")]
    [Tooltip("Object for the player")]
    [SerializeField] private Player player;

    [Header("SCREEN OBJECTS")]
    [Tooltip("GameObject for the title screen")]
    public GameObject titleScreen;
    [Tooltip("Gameobject shown during the gameplay")]
    public GameObject gamePlay;
    [Tooltip("Gameobject for the options menu")]
    public GameObject options;
    [Tooltip("Gameobject shown during the pause menu")]
    public GameObject pause;
    [Tooltip("Gameobject Shown during the post game menu")]
    public GameObject postGame;

    [Space(10)]
    [Header("GAME STATE")]
    [Tooltip("Shows the current game state")]
    public GameState currentState = GameState.Title;
    [Tooltip("Shows the previous games state")]
    public GameState previousState = GameState.Title;

    #endregion

    #region MONOBEHAVIOUR

    private void Awake() {
        if ( !Instance ) Instance = this;
        DontDestroyOnLoad(this);
        _init();
    }

    // Update is called once per frame
    void Update() {
        switch (currentState) {

            case GameState.Title:
                ClearScreen();
                titleScreen.SetActive(true);
                break;
            case GameState.Options:
                if ( previousState != GameState.Gameplay ) { ClearScreen(); }
                options.SetActive(true);
                break;
            case GameState.Gameplay:
                ClearScreen();
                gamePlay.SetActive(true);
                player.TotalTime += Time.deltaTime;
                break;
            case GameState.Pause:
                pause.SetActive(true);
                break;
            case GameState.PostGame:
                ClearScreen();
                postGame.SetActive(true);
                break;
            default:
                currentState = GameState.Title;
                _init();
                break;
        }
    }

    #endregion

    #region PUBLIC PROPERTIES

    public int PlayerHealth { get => player.Health; set => player.Health = value; }
    public int PlayerScore { get => player.Score; set => player.Score = value; }
    public float PlayerTime { get => player.TotalTime; set => player.TotalTime = value; }
    
    #endregion

    #region FUNCTIONS
    /// <summary>
    /// This function is run at the start of the game/program
    /// </summary>
    private void _init() {
        titleScreen.SetActive(true);
        gamePlay.SetActive(false);
        options.SetActive(false);
        pause.SetActive(false);
        postGame.SetActive(false);
    }

    /// <summary>
    /// A inspector button used to test game state transitions to the next state
    /// </summary>
    [ButtonGroup(TestString)]
    [Button("Next State")]
    private void NextState() {
        _stateNum++;

        if ( _stateNum >= typeof(GameState).GetEnumValues().Length ) {
            _stateNum = 0;
        }
        TransitionToState((GameState)_stateNum);
    }

    /// <summary>
    /// A inspector button used to test game state transitions to the previous state
    /// </summary>
    [ButtonGroup(TestString)]
    [Button("Prev State")]
    private void PrevState() {
        _stateNum--;

        if ( _stateNum <= 0 ) {
            _stateNum = typeof(GameState).GetEnumValues().Length;
        }
        TransitionToState((GameState)_stateNum);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    public void TransitionToState(GameState state) {
        SoundManager.Instance.PlaySound(SoundManager.SoundSelection.PlayerSelect);
        previousState = currentState;
        currentState = state;
    }

    /// <summary>
    /// Funtion used to initialise the players base data
    /// </summary>
    public void InitPlayerData() {
        Instance.PlayerHealth = 15;
        Instance.player.Score = 0;
        Instance.player.TotalTime = 0;
    }

    /// <summary>
    /// Function used to clear the screen in preparation for a state change
    /// </summary>
    private void ClearScreen() {
        titleScreen.SetActive(false);
        gamePlay.SetActive(false);
        options.SetActive(false);
        pause.SetActive(false);
        postGame.SetActive(false);
    }

    #endregion

}