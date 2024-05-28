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
            Debug.Log("Verifying object;");
            if (!interactivePainting.isAchieved)
            {
                Debug.Log("Something not achieved");
                return;
            }
        }

        GlobalContext.currentLevel++;
    }
}