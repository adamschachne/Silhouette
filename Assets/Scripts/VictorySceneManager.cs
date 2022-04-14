using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using TMPro;

public class VictorySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button nextLevelButton;
    private LevelManager levelManager;
    public TextMeshProUGUI playerStatText;
    private GameObject canvas;

    void Start()
    {
        playerStatText.text += $"{PlayerData.NumberOfSeconds} seconds";
        SendAnalytics();
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();

        canvas = GameObject.FindGameObjectWithTag("MovementControls");
        AudioManager.instance.Play("clapping");
        SoundManager.PlaySound();
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
    }

    private static void SendAnalytics()
    {
        AnalyticsSender.SendLevelFinishedEvent(PlayerData.NumberOfSeconds);
        AnalyticsSender.SendDegreesUsedInLevelEvent(PlayerData.DegreesCameraRotated);
        AnalyticsSender.SendMovesPerLevelEvent(PlayerData.NumberOfMoves, PlayerData.NumberOfRotations);
    }
}
