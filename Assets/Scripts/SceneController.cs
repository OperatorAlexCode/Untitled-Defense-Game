using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public static class SceneController
{
    static string GameSceneName = "InGame";
    static string MainMenuSceneName = "MainMenu";
    static string SettingsMenuSceneName = "SettingsMenu";
    static string GameOverSceneName = "Game Over";

    static public void LoadGameScene()
    {
        LoadScene(GameSceneName);
    }

    static public void LoadGameOverScene()
    {
        LoadScene(GameOverSceneName);
    }

    static public void LoadMainMenuScene()
    {
        LoadScene(MainMenuSceneName);
    }

    static public void LoadSettingsMenuScene()
    {
        LoadScene(SettingsMenuSceneName);
    }

    static public void ExitApplication()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    static void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
