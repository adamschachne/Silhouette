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
    public TextMeshProUGUI PlayerStatText;
    public GameObject canvas;

    void Start()
    {
        PlayerStatText = FindObjectOfType<TextMeshProUGUI>();
        PlayerStatText.text += PlayerData.NumberOfSeconds;
        AnalyticsSender.SendLevelFinishedEvent(PlayerData.CurrentLevel, PlayerData.NumberOfSeconds);
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
    }
}
