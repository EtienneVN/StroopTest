using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* TODO - Create Options settings 
* mute sounds = 0% sound, SoundManager active = false
* sound volume slider
*/

public class OptionsScreen : MonoBehaviour
{
    public GameObject resumeButton;

    private void OnEnable() {
        resumeButton.SetActive(false);
    }

    private void Update() {
        if ( GameManager._instance.previousState == GameManager.GameState.GAMEPLAY ) {
            resumeButton.SetActive(true);
        }
    }

}