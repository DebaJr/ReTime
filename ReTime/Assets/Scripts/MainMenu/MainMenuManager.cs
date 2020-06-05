using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void StartNewGame()
    {
        PlayerPrefs.SetInt("LevelsUnlocked", 1);
        TimeFlow.Time = 500;
        LevelLoadManager.LoadIntro();
        audioSource = FindObjectOfType<AudioSource>();
        DontDestroyOnLoad(audioSource);
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("LevelsUnlocked"))
        {
            TimeFlow.Time = PlayerPrefs.GetInt("TimeFlow");
            LevelLoadManager.LoadHUB();
        }
        else
        {
            GameObject.FindObjectOfType<Animator>().SetTrigger("NoGameFound");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
