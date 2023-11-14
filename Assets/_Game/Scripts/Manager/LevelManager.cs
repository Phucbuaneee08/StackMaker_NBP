using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<Level> levels = new List<Level>();
    public Player player;

    Level currentLevel;

    public int level = 1;
    private void Start()
    {
        UIManager.Instance.OpenMainMenuUI();
        LoadLevel();
    }
    public void LoadLevel()
    {
        LoadLevel(level);
        OnInit();
    }
    public void LoadLevel(int indexLevel)
    {
        UIManager.Instance.SetNextLevelText(indexLevel);
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[indexLevel - 1]);
    }

    public void NextLevel()
    {
        level++;
        LoadLevel();
    }

    public void OnInit()
    {
        player.transform.position = currentLevel.startPoint.position;
        player.destination = currentLevel.endPoint;
        player.OnInit();
    }

    public void OnStart()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }

    public void OnFinish()
    {
        UIManager.Instance.OpenFinishUI();
        GameManager.Instance.ChangeState(GameState.Finish);
    }

    public bool CheckNextLevel()
    {
        if (level <= levels.Count - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


