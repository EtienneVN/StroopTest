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

    [Header("GAMEPLAY OBJECTS")]
    public TextMeshProUGUI TestObject;
    public List<GameObject> PlayerButtons;

    [Space(5)]
    [Header("COLOURS")]
    public List<String> colours;

    [Space]
    [Header("Current Colors")]
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
        reroll();
    }

    // Update is called once per frame
    void Update() {

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

    // TODO - Check duplicate colors and change them to a different colour
    [Button("TestButtons")]
    private void testButton() {
        // Clear the button text
        buttonColors.Clear();
        foreach ( var button in PlayerButtons ) {
            button.GetComponentInChildren<Text>().text = "";
        }

        // Set Stroop Colour
        int r = UnityEngine.Random.Range(0, 4);
        PlayerButtons[r].GetComponentInChildren<Text>().text = testText;
        buttonColors.Add(testText);

        // set button colours text
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
        int r = UnityEngine.Random.Range(0, colours.Count);
        return colours[r];
    }
    String randomColour(String s) {
        int r = UnityEngine.Random.Range(0, colours.Count);
        String randCol = colours[r];

        if ( !buttonColors.Contains(randCol) ) {
            return randCol;
        }

        return randomColour(s);
    }


    private void reroll() {
        selectedColour = null;
        buttonColors.Clear();
        testColour();
        testButton();
    }

    public bool compareColours() {
        if ( selectedColour == testText ) {
            Debug.Log("Correct");
            reroll();
            return true;
        }

        Debug.Log("Incorrect");
        reroll();
        return false;
    }

    public void setSelectedColour(GameObject t) {
        selectedColour = t.GetComponentInChildren<Text>().text;
        compareColours();
    }

    public void quitToTitle() {
        GameManager.Instance.currentState = GameManager.GameState.TITLE;
    }

    #endregion

}