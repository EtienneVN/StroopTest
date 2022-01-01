using TMPro;
using UnityEngine;

public class PostGame : MonoBehaviour
{
    [Tooltip("Text object for the players score at the end of the game")]
    public TextMeshProUGUI scoreData;
    [Tooltip("Text object for the players time at the end of the game")]
    public TextMeshProUGUI timeData;
    [Tooltip("Text object for the number of rounds reached by the player ")]
    public TextMeshProUGUI roundData;

    private void OnEnable() {
        scoreData.text = GameManager.Instance.PlayerScore.ToString();
        timeData.text = GameManager.Instance.PlayerTime.ToString().Split('.')[0];
        roundData.text = GameManager.Instance.PlayerRounds.ToString();
    }
}