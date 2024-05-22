using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalContext : MonoBehaviour
{
    public static GameStatus gameStatus = GameStatus.paused;

    public static bool isAnyPanelOpen = false;

    public static int currentLevel = 0;

    public static List<Level> levels = new();

    public static void PopulateLevels()
    {
        Level[] levelComponents = FindObjectsOfType<Level>();
        levels.AddRange(levelComponents);
    }

    public static void CheckAchievements()
    {
        levels[currentLevel].CheckAchievements();
    }
    
}
