using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions.Must;

/*
 * 
 * play the sounds 
 */

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;

    #region CONSTRUCTOR

    #endregion
    #region PRIVATE MEMBERS

    public enum soundSelection
    {
        playerSelect = 1,
        wrongSelection = 2,
        transition = 3
    }

    #endregion

    #region PUBLIC MEMBERS

    [Space(15)]
    [Header("AUDIO SOUNDS")]
    public AudioSource audioSource;

    [Space(10)]
    [Header("AUDIO LIBRARY")]
    public AudioClip playerSelect;
    public AudioClip wrongSelection;
    public AudioClip transition;

    #endregion

    #region PUBLIC PROPERTIES

    //public bool 

    #endregion

    #region MONOBEHAVIOUR

    private void Awake() {
        if ( !_instance ) _instance = this;
        if ( !audioSource ) audioSource = gameObject.GetComponent<AudioSource>() ? null : gameObject.AddComponent<AudioSource>();
        audioSource.clip = null;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    #endregion

    #region FUNCTIONS

    /// <summary>
    /// Sound test button
    /// </summary>
    /// <param name="sound"></param>
    [Button("Test Selected Sound")]
    public void playSound(soundSelection sound = soundSelection.playerSelect) {
        if ( !audioSource ) audioSource = gameObject.GetComponent<AudioSource>() ? null : gameObject.AddComponent<AudioSource>();

        if ( !audioSource.isPlaying ) {
            audioSource.clip = SoundLibrary(sound);
            audioSource.Play();
        }
    }

    /// <summary>
    /// SoundLibrary : Collection of available sounds
    /// </summary>
    /// <param name="sounds"></param>
    /// <returns></returns>
    public AudioClip SoundLibrary(soundSelection sounds) {
        switch (sounds) {
            case soundSelection.playerSelect:
                return playerSelect;
            case soundSelection.wrongSelection:
                return wrongSelection;
            case soundSelection.transition:
                return transition;
            default:
                return null;
        }
    }

    #endregion
}