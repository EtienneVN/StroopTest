using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GamePlay : MonoBehaviour
{
    #region CONSTRUCTORS

    #endregion

    #region PRIVATE MEMBERS

    #endregion

    #region PUBLIC MEMBERS

    public string selectedColour;
    public String testText;

    [Space]
    [Header("TOPBAR OBJECTS")]
    public TextMeshProUGUI TimeData;
    public TextMeshProUGUI ScoreData;
    public TextMeshProUGUI HealthData;

    [Header("GAMEPLAY OBJECTS")]
    public TextMeshProUGUI TestObject;
    public List<GameObject> PlayerButtons;

    [Space(5)]
    [Header("GAMEPLAY COLOUR SETTINGS")]
    public List<String> coloursText;
    public List<Color> textColour;

    [Space]
    [Header("CURRENT GAMEPLAY COLOURS")]
    public List<String> buttonColors;

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
        GameManager._instance.initPlayerData();
        reroll();
    }

    // Update is called once per frame
    void Update() {
        // endGame();
    }

    private void FixedUpdate() {
        TimeData.text =  GameManager._instance.player.TotalTime.ToString().Split('.')[0];
        HealthData.text = GameManager._instance.player.health.ToString();
        ScoreData.text = GameManager._instance.player.score.ToString();
    }

    #endregion

    #region FUNCTIONS

    [Button("TestColour")]
    private void testColour() {
        string randColour = randomColour();

        if ( TestObject.text == randColour )
            testColour();

        TestObject.text = randomColour();
        testText = TestObject.text;

    }

    [Button("TestButtons")]
    private void testButton() {
        // Clear the button text
       // buttonColors.Clear();
        foreach ( var button in PlayerButtons ) {
            button.GetComponentInChildren<Text>().text = "";
            // button.GetComponentInChildren<Text>().color = Color.white;
        }

        // Set Stroop Colour
        int r = UnityEngine.Random.Range(0, PlayerButtons.Count);
        int c = UnityEngine.Random.Range(0, textColour.Count);
        Color testCol = textColour[c];
        testCol.a = 1;
        TestObject.color = testCol;
        PlayerButtons[r].GetComponentInChildren<Text>().text = testText;
        // PlayerButtons[r].GetComponentInChildren<Image>().color = testCol;
        buttonColors.Add(testText);

        // set button coloursText text
        // TODO - Change the color of the text and image of the buttons
        foreach ( var button in PlayerButtons ) {
            String randCol = randomColour(testText);
            c = UnityEngine.Random.Range(0, textColour.Count);
            testCol = textColour[c];
            testCol.a = 1;
            //  button.GetComponentInChildren<Image>().color = testCol;
            // button.GetComponentInChildren<Text>().color = testCol;
            if ( button.GetComponentInChildren<Text>().text == "" ) {
                buttonColors.Add(randCol);
                button.GetComponentInChildren<Text>().text = randCol;
                // button.GetComponentInChildren<Text>().color = testCol;
                // button.GetComponentInChildren<Image>().color = testCol;
            }
        }

    }


    /// <summary>
    /// Return a random colour
    /// </summary>
    /// <returns></returns>
    String randomColour() {
        int r = UnityEngine.Random.Range(0, coloursText.Count);
        return coloursText[r];
    }

    /// <summary>
    /// Random colour excluding given colour
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    String randomColour(String exclude) {
        int r = UnityEngine.Random.Range(0, coloursText.Count);
        String randCol = coloursText[r];

        if ( !buttonColors.Contains(randCol) ) {
            return randCol;
        }

        return randomColour(exclude);
    }

    /// <summary>
    /// Refresh Stroop object and buttons
    /// </summary>
    private void reroll() {
        selectedColour = null;
        buttonColors.Clear();
        testColour();
        testButton();
    }

    public bool compareSelectedColour() {
        if ( selectedColour == testText ) {
            Debug.Log("Correct");
            SoundManager._instance.playSound();
            GameManager._instance.player.score += 10;
            reroll();
            return true;
        }

        Debug.Log("Incorrect");
        SoundManager._instance.playSound(SoundManager.soundSelection.wrongSelection);
        GameManager._instance.player.health -= 5;
        GameManager._instance.player.score -= 20;
        reroll();
        return false;
    }

    void endGame() {
        if ( GameManager._instance.player.health <= 0 )
            GameManager._instance.transitionToState(GameManager.GameState.PostGame);
    }

    #endregion

    #region BUTTONS

    /// <summary>
    /// Sets colour text onto button components
    /// </summary>
    /// <param name="t"></param>
    public void setSelectedColour(GameObject t) {
        selectedColour = t.GetComponentInChildren<Text>().text;
        compareSelectedColour();
        endGame();
    }

    public void pauseButton() {
        GameManager._instance.transitionToState(GameManager.GameState.PAUSE);
    }

    public void ReturnToTitle() {
        GameManager._instance.transitionToState(GameManager.GameState.TITLE);
    }

    #endregion

}