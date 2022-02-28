using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [Scene]
    public string[] levels;
    private const string VICTORY_SCENE_NAME = "VictoryScene";
    private const string MAIN_SCENE_NAME = "MainScene";

    public string CurrentLevelName { get => levels[PlayerData.CurrentLevel]; }

    void Awake()
    {
        // Load the first level when the Main Scene starts
        SceneManager.LoadSceneAsync(CurrentLevelName, LoadSceneMode.Additive);
    }

    public void LoadVictoryScene()
    {
        SceneManager.LoadScene(VICTORY_SCENE_NAME, LoadSceneMode.Additive);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(MAIN_SCENE_NAME);
    }
}
