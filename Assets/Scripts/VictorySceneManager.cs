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
        Analytics.CustomEvent("Time Taken For Level", new Dictionary<string, object> {
            {"Level Number",  PlayerData.CurrentLevel },
            {"Time Taken", PlayerData.NumberOfSeconds }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
