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
        GameManager.Instance.initPlayerData();
        reroll();
    }

    // Update is called once per frame
    void Update() {
        // endGame();
    }

    private void FixedUpdate() {
        TimeData.text = GameManager.Instance.player.TotalTime.ToString();
        HealthData.text = GameManager.Instance.player.health.ToString();
        ScoreData.text = GameManager.Instance.player.score.ToString();
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
        buttonColors.Clear();
        foreach ( var button in PlayerButtons ) {
            button.GetComponentInChildren<Text>().text = "";
        }

        // Set Stroop Colour
        int r = UnityEngine.Random.Range(0, PlayerButtons.Count);
        int c = UnityEngine.Random.Range(0, textColour.Count);
        Color testCol = textColour[c];
        testCol.a = 1;
        PlayerButtons[r].GetComponentInChildren<Text>().text = testText;
        TestObject.GetComponentInChildren<TextMeshProUGUI>().color = testCol;
        buttonColors.Add(testText);

        // set button coloursText text
        foreach ( var button in PlayerButtons ) {
            String randCol = randomColour(testText);
            if ( button.GetComponentInChildren<Text>().text == "" ) {
                buttonColors.Add(randCol);
                button.GetComponentInChildren<Text>().text = randCol;
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
    String randomColour(String s) {
        int r = UnityEngine.Random.Range(0, coloursText.Count);
        String randCol = coloursText[r];

        if ( !buttonColors.Contains(randCol) ) {
            return randCol;
        }

        return randomColour(s);
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
            GameManager.Instance.player.score += 10;
            Debug.Log("Correct");
            reroll();
            return true;
        }

        Debug.Log("Incorrect");
        GameManager.Instance.player.health -= 5;
        GameManager.Instance.player.score -= 20;
        reroll();
        return false;
    }

    void endGame() {
        if ( GameManager.Instance.player.health <= 0 )
            GameManager.Instance.currentState = GameManager.GameState.PostGame;

    }

    /// <summary>
    /// Sets colour text onto button components
    /// </summary>
    /// <param name="t"></param>
    public void setSelectedColour(GameObject t) {
        selectedColour = t.GetComponentInChildren<Text>().text;
        compareSelectedColour();
        endGame();
    }

    public void quitToTitle() {
        GameManager.Instance.currentState = GameManager.GameState.TITLE;
    }

    #endregion

}