using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions.Must;

/// <summary>
/// Responsible for controlling all of the sound within the game 
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    #region PRIVATE MEMBERS

    private const string TestSting = "Test";
    
    [Space(15)]
    [Header("AUDIO SOURCE")]
    [Tooltip("The required Audio component")]
    [SerializeField] private AudioSource audioSource;
    
    #endregion

    #region PUBLIC MEMBERS

    SoundManager() {
        Instance = this;
    }
    
    /// <summary>
    /// An enumerable for the sounds available within the game
    /// </summary>
    public enum SoundSelection
    {
        PlayerSelect = 1,
        WrongSelection = 2,
        Transition = 3
    }

    
    [Space(10)]
    [Header("AUDIO LIBRARY")]
    [Tooltip("Audioclip for the player selection sound")]
    public AudioClip playerSelect;
    [Tooltip("Audioclip for the error selection sound")]
    public AudioClip wrongSelection;
    [Tooltip("Audioclip for the transition sound")]
    public AudioClip transition;

    #endregion

    #region PUBLIC PROPERTIES

    #endregion

    #region MONOBEHAVIOUR

    private void Awake() {
        if ( !Instance ) Instance = this;
        DontDestroyOnLoad(this);
        AudioSourceCheck();
    }

    // Update is called once per frame
    void Update() {
        // Clear Audio Source in preparation for next sound
        if ( !audioSource.isPlaying )
            audioSource.clip = null;
    }

    #endregion

    #region FUNCTIONS

    /// <summary>
    /// Checks if there is an Audio Source component
    /// This GameObject is dependent on a Audio source and therefore
    /// one will be created if it cannot be found
    /// </summary>
    void AudioSourceCheck() {
        if ( !audioSource ? ( gameObject.GetComponent<AudioSource>() ? audioSource = gameObject.GetComponent<AudioSource>() : audioSource = gameObject.AddComponent<AudioSource>() ) : true ) ;
        audioSource!.playOnAwake = false;
    }

    /// <summary>
    /// A button to test the sounds within the inspector
    /// </summary>
    /// <param name="sound"></param>
    [Button("Test Selected Sound")]
    public void PlaySound(SoundSelection sound = SoundSelection.PlayerSelect) {
        AudioSourceCheck();

        // Prevent duplicate sounds playing
        if ( audioSource.clip != SoundLibrary(sound) ) {
            audioSource.clip = SoundLibrary(sound);
            audioSource.Play();
        }
    }

    /// <summary>
    /// SoundLibrary : Collection of available sounds for the game
    /// </summary>
    /// <param name="sounds"></param>
    /// <returns></returns>
    public AudioClip SoundLibrary(SoundSelection sounds) {
        switch (sounds) {
            case SoundSelection.PlayerSelect:
                return playerSelect;
            case SoundSelection.WrongSelection:
                return wrongSelection;
            case SoundSelection.Transition:
                return transition;
            default:
                return null;
        }
    }

    #endregion
}