using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameViewController : MonoBehaviour
{
    #region Members

    [SerializeField]
    private Image healthPointReference;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private RectTransform healthPointContent;
    [SerializeField]
    private Text infoText;
    [SerializeField]
    private UserNamePanel namePanel;
    [SerializeField]
    private BestScorePanel scorePanel;
    
    #endregion
    
    #region Properties

    private RectTransform HealthPointContent {
        get => healthPointContent;
    }

    private Text ScoreText {
        get => scoreText;
    }

    private Image HealthPointReference {
        get => healthPointReference;
    } 

    private Text InfoText {
        get => infoText;
    }

    private UserNamePanel NamePanel {
        get => namePanel;
    }

    private BestScorePanel ScorePanel {
        get => scorePanel;
    }

    private List<Image> SpawnedLifes {
        get;set;
    } = new List<Image>();

    #endregion
    
    #region Methods

    private void Start()
    {
        Initialize();
        AttachEvents();        
    }

    private void Initialize()
    {
        NamePanel.Initialize(SaveAndShowBestScorePanel);
        NamePanel.gameObject.SetActive(false);
        ScorePanel.gameObject.SetActive(false);

        if(PlayerManager.Instance!=null)
        {
            UpdateHealthPoints(PlayerManager.Instance.GetLifes());
            UpdateScorePoints(PlayerManager.Instance.GetScore());
        }
    }

    private void AttachEvents()
    {
        if(GameAction.Instance!=null)
        {
            GameAction.Instance.OnDecreaseHealthPoints += UpdateHealthPoints;
            GameAction.Instance.OnIncreaseScore += UpdateScorePoints;
            GameAction.Instance.OnGameOver += ShowGameOverState;
            GameAction.Instance.OnStartGameCounterUpdate += UpdateStartCounter;
            GameAction.Instance.OnRestartGame += ResetHealthPoints;
        }
    }

    private void DetachEvents()
    {
        if(GameAction.Instance!=null)
        {
            GameAction.Instance.OnDecreaseHealthPoints -= UpdateHealthPoints;
            GameAction.Instance.OnIncreaseScore -= UpdateScorePoints;
            GameAction.Instance.OnGameOver -= ShowGameOverState;
            GameAction.Instance.OnStartGameCounterUpdate -= UpdateStartCounter;
            GameAction.Instance.OnRestartGame -= ResetHealthPoints;
        }
    }

    private void ResetHealthPoints()
    {
        UpdateHealthPoints(PlayerManager.Instance.GetLifes());
    }

    private void UpdateStartCounter(int counter)
    {
        if(counter==0) 
        {
            InfoText.text = string.Empty;
            return;
        }
        InfoText.text = counter.ToString();
    }

    private void UpdateScorePoints(int score)
    {
        ScoreText.text = string.Format("Score: {0}", score.ToString("0000"));
    }

    private void UpdateHealthPoints(int lifes)
    {
        ClearSpawnedLifes();

        for (int i = 0; i < lifes; i++)
        {
            Image lifeIcon = Instantiate(HealthPointReference);
            lifeIcon.transform.SetParent(HealthPointContent);
            SpawnedLifes.Add(lifeIcon);
        }
    }

    private void ShowGameOverState()
    {
        InfoText.text = "GAME OVER";
        StartCoroutine(ShowUserNamePanel());
    }

    private void ClearSpawnedLifes()
    {
        for (int i = 0; i < SpawnedLifes.Count; i++)
        {
            Destroy(SpawnedLifes[i].gameObject);
        }

        SpawnedLifes.Clear();
    }

    private IEnumerator ShowUserNamePanel()
    {
        yield return new WaitForSeconds(2f);

        NamePanel.gameObject.SetActive(true);
    }

    private void SaveAndShowBestScorePanel(string userName) 
    {
        BestScore userScore = new BestScore(userName, PlayerManager.Instance.GetScore());
        GameManager.Instance.TrySaveUserScore(userScore);
        NamePanel.gameObject.SetActive(false);
        ScorePanel.ShowPanel(userScore);
    }

    #endregion
    
    #region ClassesAndEnums

    #endregion 
}
