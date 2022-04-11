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
        SceneManager.LoadScene("Level 2");
    }

    public void LevelTwo() {
        // Application.LoadLevel("Level 2");
        SceneManager.LoadScene(3);
    }

    public void LevelThree() {
        // Application.LoadLevel("Level 3");
        SceneManager.LoadScene(4);
    }

    public void LevelFour() {}
    public void LevelFive() {}
}
