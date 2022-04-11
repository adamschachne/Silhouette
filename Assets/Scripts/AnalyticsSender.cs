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
    private static readonly string RESET = "Reset";
    private static readonly string NUMBER_OF_BOXES = "Number Of Boxes";
    private static readonly string TOTAL_SOLUTION_COUNT = "Total Solution Count";
    private static readonly string DEGREES_FOR_LEVEL = "Degrees For Level";
    private static readonly string DEGREES = "Degrees";
    private static readonly string NUMBER_OF_MOVES = "Number Of Moves";
    private static readonly string NUMBER_OF_ROTATIONS = "Number Of Rotations";
    private static readonly string NUMBER_OF_MOVES_PER_LEVEL = "Number Of Moves Per Level";
    private static readonly string TIME_BETWEEN_MOVES = "Time Between Moves";
    private static readonly string TIME = "Time";

    private static void CustomEvent(string customEventName, IDictionary<string, object> eventData)
    {
        // Only send the data if it is not a Debug build
        if (!Debug.isDebugBuild)
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
            Debug.LogFormat("Recieved call to send data with event name: {0}  and with data: \n {1}\n", customEventName, keyValues);

        }
    }

    private static int getCurrentLevel()
    {
        return PlayerData.CurrentLevel + 1;
    }



    public static void SendLevelReachedEvent()
    {
        AnalyticsSender.CustomEvent(LEVEL_REACHED, new Dictionary<string, object> { { LEVEL_STARTED, getCurrentLevel() } });
    }

    public static void SendLevelFinishedEvent(int numberOfSeconds)
    {
        AnalyticsSender.CustomEvent(TIME_TAKEN_FOR_LEVEL, new Dictionary<string, object> {
            {LEVEL_NUMBER,  getCurrentLevel() },
            {TIME_TAKEN, numberOfSeconds }
        });
    }

    public static void SendResetEvent(int numberOfBoxes, int totalSolutionCount)
    {
        AnalyticsSender.CustomEvent(RESET, new Dictionary<string, object> {
            {LEVEL_NUMBER,  getCurrentLevel() },
            {NUMBER_OF_BOXES, numberOfBoxes},
            {TOTAL_SOLUTION_COUNT, totalSolutionCount }
        });
    }

    public static void SendDegreesUsedInLevelEvent(float degrees)
    {
        AnalyticsSender.CustomEvent(DEGREES_FOR_LEVEL, new Dictionary<string, object> {
            {LEVEL_NUMBER,  getCurrentLevel() },
            {DEGREES, degrees}
        });
    }

    public static void SendMovesPerLevelEvent(int moves, int rotations)
    {
        AnalyticsSender.CustomEvent(NUMBER_OF_MOVES_PER_LEVEL, new Dictionary<string, object> {
            {LEVEL_NUMBER,  getCurrentLevel() },
            {NUMBER_OF_MOVES, moves},
            {NUMBER_OF_ROTATIONS, rotations }
        });
    }

    public static void SendTimeBetweenMovesEvent(int timeBetweenMoves)
    {
        AnalyticsSender.CustomEvent(TIME_BETWEEN_MOVES, new Dictionary<string, object> {
            {LEVEL_NUMBER, getCurrentLevel()  },
            {TIME, timeBetweenMoves}
        });
    }
}
