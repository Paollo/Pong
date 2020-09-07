using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BestScore
{
    #region Members

    [SerializeField]
    private string userName = string.Empty;
    [SerializeField]
    private int score =0;
    
    #endregion
    
    #region Properties

    public string UserName {
        get => userName;
        private set => userName = value;
    }

    public int Score {
        get => score;
        private set => score = value;
    }

    #endregion
    
    #region Methods

    public BestScore()
    {
        
    }

    public BestScore(string userName, int currentScore)
    {
        SetUserName(userName);
        SetBestScore(currentScore);
    }

    public void SetBestScore(int currentScore)
    {
        Score = currentScore;
    }

    public void SetUserName(string name)
    {
        UserName = name;
    }

    #endregion
    
    #region ClassesAndEnums

    #endregion 
}
