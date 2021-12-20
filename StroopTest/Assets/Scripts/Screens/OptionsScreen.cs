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
    public Toggle mute;
    public Slider volume;

    /// <summary>
    /// todo - value only changes once 
    /// </summary>
    private void Start() {
        /*if ( SoundManager._instance ) {
            mute.onValueChanged.AddListener(delegate { ToggleChange(mute); });
        }*/
    }

    private void Update() { 
        /*if ( SoundManager._instance ) {
            mute.onValueChanged.AddListener(delegate { ToggleChange(mute); });
        }*/
    }

    private void ToggleChange(Toggle toggle) {
        SoundManager._instance.audioSource.mute = toggle;
    }


}
