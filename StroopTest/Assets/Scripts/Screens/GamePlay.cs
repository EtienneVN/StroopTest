using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

/// <summary>
/// Resposible for all the script that is run during the gameplay of the game
/// </summary>
public class GamePlay : MonoBehaviour
{
    #region CONSTRUCTORS

    #endregion

    #region PRIVATE MEMBERS

    private const string TestString = "Test Buttons";

    /// <summary>
    /// used to compare the color selected by the player
    /// </summary>
    private string _selectedColour;

    #endregion

    #region PUBLIC MEMBERS

    [Tooltip("")]
    public String stroopText;

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
    public List<GameObject> playerButtons;

    [Space(5)]
    [Header("GAMEPLAY COLOUR SETTINGS")]
    [Tooltip("List strings for the colours")]
    public List<String> coloursText;
    [Tooltip("List of colours referencing the text")]
    public List<Color> textColour;

    [Space]
    [Header("CURRENT GAMEPLAY COLOURS")]
    [Tooltip("Color text given to buttons during current gameplay")]
    public List<String> buttonColors;

    #endregion

    #region MONOBEHAVIOUR

    // Start is called before the first frame update
    void Start() {
        GameManager.Instance.InitPlayerData();
        Reroll();
    }

    private void FixedUpdate() {
        timeData.text = GameManager.Instance.PlayerTime.ToString().Split('.')[0];
        healthData.text = GameManager.Instance.PlayerHealth.ToString();
        scoreData.text = GameManager.Instance.PlayerScore.ToString();
    }

    #endregion

    #region FUNCTIONS

    /// <summary>
    /// Changes the Stroop text objects text
    /// </summary>
    [ButtonGroup(TestString)]
    [Button("Test Stroop Text")]
    private void ChangeStroopColour() {
        string randColour = RandomColour();

        if ( testObject.text == randColour )
            ChangeStroopColour();

        testObject.text = RandomColour();
        stroopText = testObject.text;

        // Set stroop Colour
        int c = UnityEngine.Random.Range(0, textColour.Count);
        Color stroopCol = textColour[c];
        stroopCol.a = 1;
        testObject.color = stroopCol;
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
            button.GetComponentInChildren<Text>().text = "";
        }
        
        // Set correct stroop test text on a button
        int r = UnityEngine.Random.Range(0, playerButtons.Count);
        playerButtons[r].GetComponentInChildren<Text>().text = stroopText;
        buttonColors.Add(stroopText);

        // set player button text colour
        foreach ( var button in playerButtons ) {
            String randCol = RandomColour(stroopText);
            int c = UnityEngine.Random.Range(1, textColour.Count);
            Color randColour = textColour[c];
            randColour.a = 1;
            if ( button.GetComponentInChildren<Text>().text == "" ) {
                buttonColors.Add(randCol);
                button.GetComponentInChildren<Text>().text = randCol;
                button.GetComponentInChildren<Text>().color = randColour;
            }
        }
    }

    /// <summary>
    /// Return a random colour from the 
    /// </summary>
    /// <returns></returns>
    String RandomColour() {
        int r = UnityEngine.Random.Range(0, coloursText.Count);
        return coloursText[r];
    }

    /// <summary>
    /// Random colour excluding given colour
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    String RandomColour(String exclude) {
        int r = UnityEngine.Random.Range(0, coloursText.Count);
        String randCol = coloursText[r];

        if ( !buttonColors.Contains(randCol) ) {
            return randCol;
        }

        return RandomColour(exclude);
    }

    /// <summary>
    /// Refresh Stroop object and buttons
    /// </summary>
    private void Reroll() {
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
        if ( _selectedColour == stroopText ) {
            Debug.Log("Correct");
            SoundManager.Instance.PlaySound();
            GameManager.Instance.PlayerScore+= 10;
            Reroll();
            return true;
        }

        Debug.Log("Incorrect");
        SoundManager.Instance.PlaySound(SoundManager.SoundSelection.WrongSelection);
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
        _selectedColour = t.GetComponentInChildren<Text>().text;
        CompareSelectedColour();
        EndGame();
    }

    #endregion

}