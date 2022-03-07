using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScene()
    {
        AnalyticsSender.SendResetEvent(PlayerData.CurrentLevel, SolutionManager.GetCurrentNumberOfBoxes(), SolutionManager.GetTargetSoultion());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
