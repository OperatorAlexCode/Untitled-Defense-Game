using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public static class SceneController
{
    static readonly string GameSceneName = "InGame";
    static readonly string MainMenuSceneName = "MainMenu";
    static readonly string SettingsMenuSceneName = "SettingsMenu";
    static readonly string GameOverSceneName = "Game Over";
    static readonly string KeybindsSceneName = "KeyBinds";
    static readonly string HowToPlaySceneName = "HowToPlay";

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

    static public void LoadKeyBindsScene()
    {
        LoadScene(KeybindsSceneName);
    }

    static public void LoadHowToPlayScene()
    {
        LoadScene(HowToPlaySceneName);
    }

    static public void ExitApplication()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }

    static void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
