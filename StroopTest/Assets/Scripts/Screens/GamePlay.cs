using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

/// <summary>
/// Responsible for all the script that is run during the gameplay of the game
/// </summary>
public class GamePlay : MonoBehaviour
{
    #region CONSTRUCTORS

    public struct ColourString
    {
        public string col_name;
        public Color col_value;
    }

    #endregion

    #region PRIVATE MEMBERS

    private const string TestString = "Test Buttons";

    /// <summary>
    /// used to compare the color selected by the player
    /// </summary>
    private string _selectedColour;
    private int streak = 0;
    private float CountDownFormula;
    private String stroopText;
    private Color stroopColour;
    #endregion

    #region PUBLIC MEMBERS

    [Tooltip("Gameplay object that the player is tested on")]
    public ColourString stroopObj;

    [Space]
    [Header("TOPBAR OBJECTS")]
    [Tooltip("Text object for the gameplay time")]
    public TextMeshProUGUI timeData;
    [Tooltip("Text object for the player score")]
    public TextMeshProUGUI scoreData;
    [Tooltip("Text object for the player health")]
    public TextMeshProUGUI healthData;

    [Header("GAMEPLAY OBJECTS")]
    [Tooltip("Text object for the stroop test")]
    public TextMeshProUGUI testObject;
    [Tooltip("List of button objects for the player buttons")]
    public Button[] playerButtons;
    public GameObject PlayerPanel;
    [Tooltip("Timer for player to choose colour")]
    public TextMeshProUGUI Countdown;

    [Space(5)]
    [Header("GAMEPLAY COLOUR SETTINGS")]
    [Tooltip("Colour and text combination text")]
    public List<ColourString> colourCombinations;

    [Space]
    [Header("CURRENT GAMEPLAY COLOURS")]
    [Tooltip("Color text given to buttons during current gameplay")]
    public List<String> buttonColors;

    #endregion

    #region MONOBEHAVIOUR

    private void Awake() {
        GameManager.Instance.InitPlayerData();
    }

    // Start is called before the first frame update
    void Start() {
        playerButtons = PlayerPanel.GetComponentsInChildren<Button>();
        Reroll();
    }

    private void FixedUpdate() {
        timeData.text = GameManager.Instance.PlayerTime.ToString().Split('.')[0];
        healthData.text = GameManager.Instance.PlayerHealth.ToString();
        scoreData.text = GameManager.Instance.PlayerScore.ToString();
        Countdown.text = GameManager.Instance.PlayerCountdown.ToString().Split('.')[0];
        checkCountdown();
        EndGame();
    }

    #endregion

    #region FUNCTIONS

    /// <summary>
    /// Changes the Stroop text objects text
    /// </summary>
    [ButtonGroup(TestString)]
    [Button("Test Stroop Text")]
    private void ChangeStroopColour() {
        string randColour = RandomColourString();

        if ( testObject.text == randColour )
            ChangeStroopColour();

        testObject.text = RandomColourString();
        stroopText = testObject.text;

        // Set stroop Colour
        int c = UnityEngine.Random.Range(0, colourCombinations.Count);
        stroopObj = colourCombinations[c];
        stroopText = stroopObj.col_name;
        stroopColour = stroopObj.col_value;
        stroopColour.a = 1;
        testObject.color = stroopColour;
    }

    /// <summary>
    /// Sets the text and colours for the buttons that the player selects
    /// </summary>
    [ButtonGroup(TestString)]
    [Button("Test Player Buttons")]
    private void SetPlayerButtons() {
        // Clear player button text 
        buttonColors.Clear();
        foreach ( var button in playerButtons ) {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        // Set correct stroop test text on a button
        int r = UnityEngine.Random.Range(0, playerButtons.Length - 1);
        playerButtons[r].GetComponentInChildren<TextMeshProUGUI>().text = stroopText;
        buttonColors.Add(stroopText);

        // set player button text colour
        foreach ( var button in playerButtons ) {
            String randCol = RandomColourString(stroopText);
            int c = UnityEngine.Random.Range(1, colourCombinations.Count - 1);
            Color randColour = colourCombinations[c].col_value;
            randColour.a = 1;
            if ( button.GetComponentInChildren<TextMeshProUGUI>().text == "" ) {
                buttonColors.Add(randCol);
                button.GetComponentInChildren<TextMeshProUGUI>().text = randCol;
                button.GetComponentInChildren<TextMeshProUGUI>().color = randColour;
            }
        }
    }

    /// <summary>
    /// Reduce player health if the countdown timer runs out of time
    /// </summary>
    private void checkCountdown() {
        // Punish player if timer runs out
        if ( GameManager.Instance.PlayerCountdown <= 0 ) {
            GameManager.Instance.PlayerHealth -= 5;
            SoundManager.Instance.PlaySound(SoundManager.SoundSelection.WrongSelection);
            streak = 0;
            Reroll();
        }
    }

    /// <summary>
    /// Reset the countdown timer
    /// </summary>
    private void resetCountdown() {
        float t = 100 % GameManager.Instance.PlayerTime > 0 ? t = 100 % GameManager.Instance.PlayerTime : t = 5f;
        CountDownFormula = ( 5 - t ) + streak;
        if ( CountDownFormula > 3 )
            GameManager.Instance.PlayerCountdown = CountDownFormula;
        else {
            GameManager.Instance.PlayerCountdown = 5f + streak;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ColourString RandomColourObj() {
        int r = UnityEngine.Random.Range(0, colourCombinations.Count - 1);
        return colourCombinations[r];
    }

    /// <summary>
    /// Return a random colour from the 
    /// </summary>
    /// <returns></returns>
    String RandomColourString() {
        int r = UnityEngine.Random.Range(0, colourCombinations.Count - 1);
        return colourCombinations[r].col_name;
    }

    /// <summary>
    /// Random colour excluding given colour
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    String RandomColourString(String exclude) {
        int r = UnityEngine.Random.Range(0, colourCombinations.Count - 1);
        String randCol = colourCombinations[r].col_name;

        if ( !buttonColors.Contains(randCol) ) {
            return randCol;
        }

        return RandomColourString(exclude);
    }

    /// <summary>
    /// Refresh Stroop object and buttons
    /// </summary>
    private void Reroll() {
        resetCountdown();
        _selectedColour = null;
        buttonColors.Clear();
        ChangeStroopColour();
        SetPlayerButtons();
    }

    /// <summary>
    /// Compares the player selected text with the text from the stroop test object
    /// </summary>
    /// <returns></returns>
    public bool CompareSelectedColour() {
        if ( _selectedColour == stroopObj.col_name ) {
            Debug.Log("Correct");
            streak++;
            SoundManager.Instance.PlaySound();
            GameManager.Instance.PlayerScore += 10;
            Reroll();
            return true;
        }

        Debug.Log("Incorrect");
        SoundManager.Instance.PlaySound(SoundManager.SoundSelection.WrongSelection);
        streak = 0;
        GameManager.Instance.PlayerHealth -= 5;
        GameManager.Instance.PlayerScore -= 20;
        Reroll();
        return false;
    }

    /// <summary>
    /// Ends the game if the players health is 0
    /// Moves the game state to the Post Game screen
    /// </summary>
    void EndGame() {
        if ( GameManager.Instance.PlayerHealth <= 0 )
            GameManager.Instance.TransitionToState(GameManager.GameState.PostGame);
    }

    /// <summary>
    /// Player button event that sets the players colour according to the button
    /// they have selected
    /// </summary>
    /// <param name="t"></param>
    public void SetSelectedColour(GameObject t) {
        _selectedColour = t.GetComponentInChildren<TextMeshProUGUI>().text;
        CompareSelectedColour();
        EndGame();
    }

    #endregion

}