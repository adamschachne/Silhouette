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
        // Load the current level when the Main Scene starts
        var loadLevel = SceneManager.LoadSceneAsync(CurrentLevelName, LoadSceneMode.Additive);
        loadLevel.completed += (result) =>
        {
            SolutionManager solutionManager = GameObject.Find("SolutionManager").GetComponent<SolutionManager>();
            foreach (GameObject wallSolution in solutionManager.wallSolutions)
            {
                string wallName = wallSolution.name.Substring(0, wallSolution.name.IndexOf(" Solution"));
                GameObject.Find(wallName).GetComponent<Wall>().enabled = true;
            }
        };
    }

    public void LoadVictoryScene()
    {
        SceneManager.LoadScene(VICTORY_SCENE_NAME, LoadSceneMode.Additive);
    }

    public void LoadLevel()
    {
        SceneManager.UnloadSceneAsync(VICTORY_SCENE_NAME);
        SceneManager.LoadScene(MAIN_SCENE_NAME);
    }
}
