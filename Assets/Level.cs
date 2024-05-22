using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelNumber;
    public List<InteractivePainting> interactivePaintings;
    
    public void CheckAchievements()
    {
        foreach (InteractivePainting interactivePainting in interactivePaintings)
        {
            if (!interactivePainting.isAchieved)
            {
                return;
            }
            
        }

        GlobalContext.currentLevel++;
        Debug.Log("Level completed!");
    }
}