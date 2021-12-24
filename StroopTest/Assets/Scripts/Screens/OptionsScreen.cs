using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class OptionsScreen : MonoBehaviour
{
    [Tooltip("Reference to the resume button")]
    public GameObject resumeButton;

    private void OnEnable() {
        resumeButton.SetActive(false);
    }

    private void Update() {
        if ( GameManager.Instance.previousState == GameManager.GameState.Gameplay ) {
            resumeButton.SetActive(true);
        }
    }

}