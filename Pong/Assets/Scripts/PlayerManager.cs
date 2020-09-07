using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Members

    [SerializeField]
    private int lifes = 3;
    [SerializeField]
    private int score = 0;
    
    #endregion
    
    #region Properties

    public static PlayerManager Instance {
        get;
        private set;
    }

    private int Lifes {
        get => lifes;
        set => lifes = value;
    }

    private int Score {
        get => score;
        set => score = value;
    }

    #endregion
    
    #region Methods

    public void Restart()
    {
        Lifes = 3;
        Score = 0;
    }

    public int GetLifes()
    {
        return Lifes;
    }

    public int GetScore()
    {
        return Score;
    }

    public bool CanStart()
    {
        return GetLifes() > 0;
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

    private void Start()
    {
        AttacheEvents();
    }

    private void OnDestroy()
    {
        DetachEvents();
    }

    private void AttacheEvents()
    {
        if(GameAction.Instance != null)
        {
            GameAction.Instance.OnPlayerLose += PlayerLoseHandler;
            GameAction.Instance.OnBounceBall += BounceBallHandler;
        }
    }

    private void DetachEvents()
    {
        if(GameAction.Instance != null)
        {
            GameAction.Instance.OnPlayerLose -= PlayerLoseHandler;
            GameAction.Instance.OnBounceBall -= BounceBallHandler;
        }
    }

    private void PlayerLoseHandler()
    {
        Lifes--;

        if(GameAction.Instance != null)
        {
           GameAction.Instance.NotifyDecreaseHealthPoints(Lifes);
        }

        if(Lifes <= 0)
        {
            if(GameAction.Instance != null)
            {
                GameAction.Instance.NotifyGameOver();
            }
            return;
        }        
    }

    private void BounceBallHandler()
    {
        Score++;
        
        if(GameAction.Instance != null)
        {
            GameAction.Instance.NotifyIncreaseScore(Score);
        }
    }

    #endregion
    
    #region ClassesAndEnums

    #endregion 
}
