using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using TMPro;

public class VictorySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button nextLevelButton;
    private LevelManager levelManager;
    public TextMeshProUGUI playerStatText;
    private GameObject canvas;
    public Timer timer;

    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();
        playerStatText.text += timer.GetElapsedTimeToDisplay();
        SendAnalytics();
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();

        canvas = GameObject.FindGameObjectWithTag("MovementControls");
        canvas.SetActive(false);

        if (PlayerData.CurrentLevel == levelManager.levels.Length - 1)
        {
            nextLevelButton.gameObject.SetActive(false);
        }
    }

    public void StartNextLevel()
    {
        PlayerData.CurrentLevel += 1;
        levelManager.LoadLevel();
        timer.ResetTimer();
    }

    private static void SendAnalytics()
    {
        AnalyticsSender.SendLevelFinishedEvent(PlayerData.NumberOfSeconds);
        AnalyticsSender.SendDegreesUsedInLevelEvent(PlayerData.DegreesCameraRotated);
        AnalyticsSender.SendMovesPerLevelEvent(PlayerData.NumberOfMoves, PlayerData.NumberOfRotations);
    }
}
