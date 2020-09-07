using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameAction : MonoBehaviour
{
    #region Members

    public event Action OnPlayerLose = delegate {};
    public event Action OnBounceBall = delegate {};
    public event Action<int> OnDecreaseHealthPoints = delegate {};
    public event Action OnGameOver = delegate {};
    public event Action<int> OnIncreaseScore = delegate {};
    public event Action<int> OnStartGameCounterUpdate = delegate {};
    public event Action OnRestartGame = delegate {};
    
    #endregion
    
    #region Properties

    public static GameAction Instance{
        get;
        private set;
    }

    #endregion
    
    #region Methods

    public void NotifyRestartGame()
    {
        OnRestartGame();
    }

    public void NotifyStartGameCounterUpdate(int counter)
    {
        OnStartGameCounterUpdate(counter);
    }

    public void NotifyIncreaseScore(int points)
    {
        OnIncreaseScore(points);
    }

    public void NotifyDecreaseHealthPoints(int healthPoints)
    {
        OnDecreaseHealthPoints(healthPoints);
    }

    public void NotifyGameOver()
    {
        OnGameOver();
    }

    public void NotifyOnPlayerLose()
    {
        OnPlayerLose();
    }

    public void NotifyOnBounceBall()
    {
        OnBounceBall();
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;        
    }

    #endregion
    
    #region ClassesAndEnums

    #endregion 
}
