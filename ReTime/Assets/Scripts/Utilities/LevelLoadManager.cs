using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelLoadManager
{
    static public void LoadMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("Menu");
    }

    static public void LoadIntro()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("IntroScene");
    }

    static public void LoadHUB()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("HUB");
    }

    static public void LoadLevel(int _levelToBeLoadedIndex, bool cursorVisible)
    {
        Cursor.visible = cursorVisible;
        SceneManager.LoadScene(_levelToBeLoadedIndex);
    }

    static public void LoadLevel(string _levelToBeLoadedIndex, bool cursorVisible)
    {
        Cursor.visible = cursorVisible;
        SceneManager.LoadScene(_levelToBeLoadedIndex);
    }

    static public void LoadEndGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("EndScene");
    }

    static public void QuitGame()
    {
        Application.Quit();
    }
}
