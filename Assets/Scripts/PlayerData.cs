using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static int NumberOfSeconds { get; set; }
    public static int CurrentLevel { get; set; }
    public static int NumberOfResetsForLevel { get; set; }
    public static HashSet<int> LevelsStarted { get; set; } = new HashSet<int>();
    public static float DegreesCameraRotated { get; set; }
    public static int NumberOfMoves { get; set; }
    public static int NumberOfRotations { get; set; }
}
