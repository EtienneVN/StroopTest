using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
 
    #region BUTTONS

    public void resumeButton() {
        GameManager._instance.Pause.SetActive(false);
        GameManager._instance.transitionToState(GameManager.GameState.GAMEPLAY);
    }
    

    #endregion
}
