using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostGame : MonoBehaviour
{
    public TextMeshProUGUI scoreData;
    public TextMeshProUGUI timeData;
    
    // Start is called before the first frame update
    void Start() {
        scoreData.text = GameManager._instance.player.score.ToString();
        timeData.text = GameManager._instance.player.TotalTime.ToString().Split('.')[0];
    }
}
