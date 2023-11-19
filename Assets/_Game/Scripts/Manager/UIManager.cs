using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text pointText;
    [SerializeField] private GameObject finishUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject gamePlay;
    [SerializeField] private GameObject LoseUI;
    [SerializeField] private Image rollEffect;
    [SerializeField] private Text currentLevel;
    [SerializeField] private Text nextLevel;
    [SerializeField] private CountDown countDown;


    private bool isActive;

    private void OnInit()
    {
        coinText.text = "0";
        isActive = false;
       
    }

    void Start()
    {
        OnInit();
    }
    public void StartCountDown()
    {
        countDown.StartCountDown();
    }
    public void SetNextLevelText(int level)
    {
        LoseUI.SetActive(false);
        nextLevel.text = "Level " + level.ToString();
    }
    public void SetCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
    public void SetPoint(int point)
    {
        pointText.text = point.ToString();
    }
    public void OpenFinishUI()
    {
        gamePlay.SetActive(false);
        finishUI.SetActive(true);
        isActive = true;
        currentLevel.text = "Level " + LevelManager.Instance.level.ToString();
    }
    public void OpenMainMenuUI()
    {
        mainMenuUI.SetActive(true);

    }
    public void OpenLoseUI()
    {
        LoseUI.SetActive(true);
        StartCountDown();
    }
   
    public void OpenGamePlay()
    {
        gamePlay.SetActive(true);
    }
    public void ClickPlayButton()
    {
        mainMenuUI.SetActive(false);
        gamePlay.SetActive(true);
        LevelManager.Instance.OnStart();
    }
    public void ClickPlayAgainButton()
    {
        LevelManager.Instance.LoadLevel();
        StartNewGame();
    }
    private void StartNewGame()
    {
        Camera.main.GetComponent<CameraFL>().ChangeState(CameraState.Start);
        mainMenuUI.SetActive(true);
        finishUI.SetActive(false);
        gamePlay.SetActive(false);
    }
    public void ClickNextLevelButton()
    {
        if (LevelManager.Instance.CheckNextLevel())
        {
            LevelManager.Instance.NextLevel();
            GameManager.Instance.ChangeState(GameState.MainMenu);
            StartNewGame();
        }
        else
        {
            GameManager.Instance.ChangeState(GameState.Finish);

        }
    }

    void Update()
    {
        if (isActive)
        {
            rollEffect.rectTransform.localRotation *= Quaternion.Euler(0f, 0f, 180f * Time.deltaTime);

        }
    }
}
