using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsSender 
{
    public static void CustomEvent(string customEventName, IDictionary<string, object> eventData)
    {
        // Only send the data if it is not a Debug build
        if(!Debug.isDebugBuild)
        {
            Analytics.CustomEvent(customEventName, eventData);   
        } else
        {
            string keyValues = "";
            foreach (string k in eventData.Keys)
            {
                keyValues += k + ": " + eventData[k] + "\n";
            }
            Debug.Log("Recieved call to send data with event name: " + customEventName + "and with data: \n" + keyValues);
            
        }
    }
}
