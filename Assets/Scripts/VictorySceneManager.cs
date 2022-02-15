using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class VictorySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text VictoryText;
    void Start()
    {
        VictoryText.text += PlayerData.NumberOfSeconds;
        AnalyticsSender.SendLevelFinishedEvent(PlayerData.CurrentLevel, PlayerData.NumberOfSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
