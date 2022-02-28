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
    public Camera mainCamera;

    void Awake()
    {
        // Load the first level when the Main Scene starts
        SceneManager.LoadSceneAsync(CurrentLevelName, LoadSceneMode.Additive);
    }

    private void UnloadCurrentLevel(System.Action<AsyncOperation> loadNextLevel)
    {
        // Unload the current level
        if (SceneManager.GetSceneByName(CurrentLevelName).isLoaded == true)
        {
            AsyncOperation unload = SceneManager.UnloadSceneAsync(CurrentLevelName);
            unload.completed += loadNextLevel;
        }
    }

    public void LoadVictoryScene()
    {
        SceneManager.LoadScene(VICTORY_SCENE_NAME, LoadSceneMode.Additive);
    }

    public void LoadLevel()
    {
        // Set the next level and load it
        //UnloadCurrentLevel((result) =>
        //{
        //    PlayerData.CurrentLevel = levelNum;
        //    SceneManager.LoadSceneAsync(CurrentLevelName, LoadSceneMode.Additive);
        //});

        //if (SceneManager.GetSceneByName(VICTORY_SCENE_NAME).isLoaded == true)
        //{
        //    SceneManager.UnloadSceneAsync(VICTORY_SCENE_NAME);
        //}

        SceneManager.LoadScene(MAIN_SCENE_NAME);
    }
}
