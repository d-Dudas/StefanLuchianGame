using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalContext : MonoBehaviour
{
    public static GameStatus gameStatus = GameStatus.paused;

    public static bool isAnyPanelOpen = false;

    public static int currentLevel = 1;

    public static List<Level> levels = new();

    public static void PopulateLevels()
    {
        Level[] levelComponents = FindObjectsOfType<Level>();
        levels.AddRange(levelComponents);
        levels.Sort((a, b) => a.levelNumber.CompareTo(b.levelNumber));
        for(int i = 0; i < levels.Count; i++)
        {
            Debug.Log("level number: " + levels[i].levelNumber);
        }
    }

    public static void CheckAchievements()
    {
        Debug.Log("Total number of levels: " + levels.Count);
        Debug.Log("CurrentLevel: " + currentLevel);
        levels[currentLevel-1].CheckAchievements();
    }
    
}
