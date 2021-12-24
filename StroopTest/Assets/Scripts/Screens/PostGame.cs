using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostGame : MonoBehaviour
{
    [Tooltip("Text object for the players score at the end of the game")]
    public TextMeshProUGUI scoreData;
    [Tooltip("Text object for the players time at the end of the game")]
    public TextMeshProUGUI timeData;

    private void OnEnable() {
        scoreData.text = GameManager.Instance.PlayerScore.ToString();
        timeData.text = GameManager.Instance.PlayerTime.ToString().Split('.')[0];
    }
}