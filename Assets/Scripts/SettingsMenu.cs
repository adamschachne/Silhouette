using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    private LevelManager levelManager;

    void Start() {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
    }    

    public void LevelOne() {
        // Debug.Log("What level is it " + PlayerData.CurrentLevel);

        PlayerData.CurrentLevel = 0;
        levelManager.LoadLevel();
    }

    public void LevelTwo() {
        PlayerData.CurrentLevel = 1;
        levelManager.LoadLevel();
    }

    public void LevelThree() {
        PlayerData.CurrentLevel = 2;
        levelManager.LoadLevel();
    }

    public void LevelFour() {
        PlayerData.CurrentLevel = 3;
        levelManager.LoadLevel();

        // SceneManager.LoadScene(6, LoadSceneMode.Additive);
    }

    public void tutorial() {
        PlayerData.CurrentLevel = 1;
        levelManager.LoadLevel();
        // SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
