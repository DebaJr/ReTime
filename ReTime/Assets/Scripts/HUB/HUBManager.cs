using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUBManager : MonoBehaviour
{
    [Tooltip("Image to be filled in relation to TimeFlow.Time")][SerializeField] Image timeFlowFill;
    [SerializeField] GameObject[] levels;

    void Start()
    {
        if(TimeFlow.Time > 0)
        {
            timeFlowFill.fillAmount = TimeFlow.Time / 1000f;
            DisableLockedLevels();
            EnableUnlockedLevels();
        }
        else
        {
            LevelLoadManager.LoadEndGame();
        }
        
    }

    void EnableUnlockedLevels()
    {
        if(PlayerPrefs.HasKey("LevelsUnlocked"))
        {
            for (int i = 0; i <= PlayerPrefs.GetInt("LevelsUnlocked"); i++)
            {
                levels[i].SetActive(true);
            }
        }
        else
        {
            foreach (GameObject _level in levels)
            {
                _level.SetActive(true);
            }
        }
    }

    void DisableLockedLevels()
    {
        foreach(GameObject _level in levels)
        {
            _level.SetActive(false);
        }
    }

    public void QuitOnHUB()
    {
        LevelLoadManager.QuitGame();
    }
}
