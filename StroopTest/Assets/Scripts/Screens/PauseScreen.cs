using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region BUTTONS

    public void resumeButton() {
        GameManager.Instance.Pause.SetActive(false);
        GameManager.Instance.transitionToState(GameManager.GameState.GAMEPLAY);
    }
    

    #endregion
}
