using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Members

    public static GameManager Instance{
        get;private set;
    }

    [SerializeField]
    private Ball ballReference;
    [SerializeField]
    private Racket racketReference;
    [SerializeField]
    private float borderOffset = 5f;
    [SerializeField]
    private bool isMainMenu = true;

    [Space]
    [Header("== Best Score Section ==")]
    [SerializeField]
    private BestScore bestUserScore;

    #endregion
    
    #region Properties

    public Vector2 BottomLeft {
        get;
        set;
    }

    public Vector2 TopRight {
        get;
        set;
    }

    public float BorderOffset {
        get => borderOffset;
    }

    public BestScore BestUserScore {
        get => bestUserScore;
        private set => bestUserScore = value;
    }

    public bool IsMainMenu {
        get => isMainMenu;
        private set => isMainMenu = value;
    }

    private Ball BallReference {
        get => ballReference;
    }

    private Racket RacketReference {
        get => racketReference;
    }

    private Camera CurrentCamera {
        get;
        set;
    }   

    private Racket CacheRacket {
        get;set;
    } 

    private Ball CacheBall {
        get;set;
    }

    #endregion
    
    #region Methods

    public void TrySaveUserScore(BestScore bestScore)
    {
        if (BestUserScore.Score < bestScore.Score)
        {
            BestUserScore = bestScore;
            SerializeObjectHelper.Serialize(BestUserScore);
        }
    }

    public void RestartGame()
    {
        PlayerManager.Instance.Restart();
        SpawnRacket();

        if(GameAction.Instance!=null)
        {
            GameAction.Instance.NotifyRestartGame();
        }

        Restart();
    }

    public void StartNewGame()
    {
        SetMainMenu(false);
        Destroy(CacheBall.gameObject);
        SceneManager.LoadScene("Game");
        SpawnRacket();
        StartCoroutine(StartGame());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SetMainMenu(true);
        SceneManager.LoadScene("MainMenu");
        SpawnBall();
    }

    public void SetMainMenu(bool flag)
    {
        IsMainMenu = flag;
    }

    private void Awake()
    {
        if(Instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;        
        CurrentCamera = Camera.main;
    }

    private void Start()
    {
        Initialize();
        AttachEvents();
    }

    private void Initialize()
    {
        BestUserScore = SerializeObjectHelper.Deserialize<BestScore>();
        BottomLeft = CurrentCamera.ScreenToWorldPoint(new Vector2(0,0));
        TopRight = CurrentCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        if(IsMainMenu == true)
        {
            //SpawnRacket();
            //StartCoroutine(StartGame());
            SpawnBall();
        }
        //SpawnBall();
    }

    private void AttachEvents()
    {
        if(GameAction.Instance!=null)
        {
            GameAction.Instance.OnPlayerLose += Restart;
            GameAction.Instance.OnGameOver += DestroyRacket;
        }   
    }

    private void Restart()
    {
        if(PlayerManager.Instance!=null && PlayerManager.Instance.CanStart() == true)
        {
            StartCoroutine(StartGame());
        }        
    }

    private void DestroyRacket() 
    {
        Destroy(CacheRacket.gameObject);
        CacheRacket = null;
    }

    private IEnumerator StartGame()
    {
        int counter = 3;

        while (counter>0)
        {
            if(GameAction.Instance!=null)
            {
                GameAction.Instance.NotifyStartGameCounterUpdate(counter);
            }  
            yield return new WaitForSeconds(1f);
            counter--;
        }

        if(GameAction.Instance!=null)
        {
            GameAction.Instance.NotifyStartGameCounterUpdate(counter);
        }  
        yield return new WaitForSeconds(.2f);

        SpawnRacket();
        SpawnBall();
    }

    private void SpawnBall()
    {
        CacheBall = Instantiate(BallReference);
        CacheBall.transform.SetParent(transform);
    }

    private void SpawnRacket()
    {
        if(CacheRacket!=null)
        {
            return;
        }

        CacheRacket = Instantiate(RacketReference);
        CacheRacket.transform.SetParent(transform);
        CacheRacket.Initialize();
    }

    #endregion
    
    #region ClassesAndEnums

    #endregion 
   
}
