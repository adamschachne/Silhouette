using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [Scene]
    public string[] levels;
    public static readonly string TUTORIAL_COMPLETE_SCENE_NAME = "TutorialCompleteScene";
    public static readonly string VICTORY_SCENE_NAME = "VictoryScene";
    public static readonly string MAIN_SCENE_NAME = "MainScene";

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

    public void LoadTutorialCompleteScene()
    {
        SceneManager.LoadScene(TUTORIAL_COMPLETE_SCENE_NAME, LoadSceneMode.Additive);
    }

    public void LoadLevel()
    {
        if (SceneManager.GetSceneByName(TUTORIAL_COMPLETE_SCENE_NAME).isLoaded)
        {
            SceneManager.UnloadSceneAsync(TUTORIAL_COMPLETE_SCENE_NAME);
        }
        else if (SceneManager.GetSceneByName(VICTORY_SCENE_NAME).isLoaded)
        {
            // necessary to trigger the old scene's OnDestroy()
            SceneManager.UnloadSceneAsync(VICTORY_SCENE_NAME);
        }
        
        SceneManager.LoadScene(MAIN_SCENE_NAME);
    }
}
