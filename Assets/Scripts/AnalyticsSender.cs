using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsSender 
{
    private static readonly string LEVEL_REACHED = "Level Reached";
    private static readonly string LEVEL_STARTED = "Level Started";
    private static readonly string TIME_TAKEN_FOR_LEVEL = "Time Taken For Level";
    private static readonly string LEVEL_NUMBER = "Level Number";
    private static readonly string TIME_TAKEN = "Time Taken";

    private static void CustomEvent(string customEventName, IDictionary<string, object> eventData)
    {
        // Only send the data if it is not a Debug build
        if(!Debug.isDebugBuild)
        {
            Analytics.CustomEvent(customEventName, eventData);   
        } 
        else
        {
            string keyValues = "";
            foreach (string k in eventData.Keys)
            {
                keyValues += k + ": " + eventData[k] + "\n";
            }
            Debug.Log("Recieved call to send data with event name: " + customEventName + "and with data: \n" + keyValues);
            
        }
    }



    public static void SendLevelReachedEvent(int levelNumber)
    {
        AnalyticsSender.CustomEvent(LEVEL_REACHED, new Dictionary<string, object> { { LEVEL_STARTED, levelNumber } });
    }

    public static void SendLevelFinishedEvent(int levelNumber, int numberOfSeconds) {
        AnalyticsSender.CustomEvent(TIME_TAKEN_FOR_LEVEL, new Dictionary<string, object> {
            {LEVEL_NUMBER,  levelNumber },
            {TIME_TAKEN, numberOfSeconds }
        });
    }
}
