using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string MAIN_SCENE_NAME = "MainScene";
    public void LoadLevel(int levelNumber)
    {

        PlayerData.CurrentLevel = levelNumber;
        SceneManager.LoadScene(MAIN_SCENE_NAME);
    }
}
