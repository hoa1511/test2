using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentLevelIndex = 1;
    public static GameManager Instance;
    private void Awake() 
    {
        Application.targetFrameRate = 80;

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        PlayerPrefs.SetInt("level", currentLevelIndex);
    }

    public void LoadNextLevel()
    {
        currentLevelIndex = PlayerPrefs.GetInt("level");
        Manager.Load("GameScene" + currentLevelIndex);
        currentLevelIndex++;
        SetLevel("level", currentLevelIndex);
    }

    public void SetLevel(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    public int GetLevel(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }

    public void ReloadLevel()
    {
        string level = (SceneManager.GetActiveScene().buildIndex).ToString();
        Manager.Load("GameScene" + level[level.Length - 1]);
    }

    public void BackToHomeScene()
    {
        Manager.Load(HomeSceneController.HOMESCENE_SCENE_NAME);
    }

    public void OpenMenuScene()
    {
        Manager.Add(PauseSceneController.PAUSESCENE_SCENE_NAME);
    }
}
