using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScorePanel : MonoBehaviour
{
    #region Members

    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private Text userScore;    
    
    #endregion
    
    #region Properties

    private Text BestScore {
        get => bestScore;
    }

    private Text UserScore {
        get => userScore;
    }

    #endregion
    
    #region Methods

    public void ShowPanel(BestScore userScore)
    {
        BestScore best = GameManager.Instance.BestUserScore;
        Debug.Log(best);
        BestScore.text = string.Format("{0}: {1}", best.UserName, best.Score.ToString("0000"));
        UserScore.text = string.Format("You - {0}: {1}", userScore.UserName, userScore.Score.ToString("0000"));
        gameObject.SetActive(true);
    }

    public void RestartClicked() 
    {
        gameObject.SetActive(false);
        GameManager.Instance.RestartGame();
    }

    public void ExitGameClicked()
    {
        gameObject.SetActive(false);
        GameManager.Instance.ReturnToMainMenu();
    }

    #endregion
    
    #region ClassesAndEnums

    #endregion 
}
