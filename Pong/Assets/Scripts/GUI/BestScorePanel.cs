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
        BestScore.text = string.Format(Constants.BEST_SCORE_FORMAT, best.UserName, best.Score.ToString(Constants.SCORE_FORMAT));
        UserScore.text = string.Format(Constants.USER_SCORE_FORMAT, userScore.UserName, userScore.Score.ToString(Constants.SCORE_FORMAT));
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
