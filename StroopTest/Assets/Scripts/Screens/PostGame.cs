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
        scoreData.text = GameManager.Instance.player.score.ToString();
        timeData.text = GameManager.Instance.player.TotalTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
