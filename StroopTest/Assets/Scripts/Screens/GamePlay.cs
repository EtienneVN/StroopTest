using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
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
        TestObject.text = randomColour();
        testText = TestObject.text;
    }

    [Button("TestButtons")]
    private void testButton() {
        foreach ( var button in PlayerButtons ) {
            button.GetComponentInChildren<Text>().text = randomColour();
        }
    }

    String randomColour() {
        int r = UnityEngine.Random.Range(0, colours.Count);
        return colours[r];
    }

    private void reroll() {
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