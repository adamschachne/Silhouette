using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void PlayGame() {
        // load current level
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void LevelOne() {
        // Application.LoadLevel("Level 1");
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }

    public void LevelTwo() {
        // Application.LoadLevel("Level 2");
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
    }

    public void LevelThree() {
        // Application.LoadLevel("Level 3");
        SceneManager.LoadScene(5, LoadSceneMode.Additive);
    }

    public void LevelFour() {
        SceneManager.LoadScene(6, LoadSceneMode.Additive);
    }
    // public void LevelFive() {}

    public void tutorial() {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
