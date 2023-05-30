using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSceneController : MonoBehaviour
{
    //Slider
    [SerializeField]
    Slider MusicVolume;
    [SerializeField]
    Slider SfxVolume;
    [SerializeField]
    Slider MouseSensitivity;
    [SerializeField]
    Slider RotationSpeed;
    [SerializeField]
    Slider SlowDownStrength;

    // Other
    PlayerSettings Settings;
    [SerializeField]
    Toggle KeyboardAimingMode;
    [SerializeField]
    TMP_InputField PlayerColor;

    private void Awake()
    {
        GameObject.Find("MainMenu").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadMainMenuScene);
        GameObject.Find("Change Keybinds").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadKeyBindsScene);

        Settings = GameObject.Find("PlayerSettings").gameObject.GetComponent<PlayerSettings>();

        UpdateSettingsScene();
    }

    public void ResetToDefault()
    {
        Settings.SetToDefaultSettings();
        UpdateSettingsScene();
    }

    public void SaveSettings()
    {
        UpdatePlayerSettings();
        Settings.SaveSettings();
    }

    void UpdatePlayerSettings()
    {
        Settings.MusicVolume = MusicVolume.value;
        Settings.SfxVolume = SfxVolume.value;
        Settings.MouseSensitivity = MouseSensitivity.value;
        Settings.RotationSpeed = RotationSpeed.value;
        Settings.SlowDownStrength = SlowDownStrength.value;
        Settings.KeyboardAimingMode = KeyboardAimingMode.isOn;

        Color color;
        if (ColorUtility.TryParseHtmlString(PlayerColor.text, out color) && PlayerColor.text != "")
            Settings.PlayerColor = color;
    }

    void UpdateSettingsScene()
    {
        MusicVolume.value = Settings.MusicVolume;
        SfxVolume.value = Settings.SfxVolume;
        MouseSensitivity.value = Settings.MouseSensitivity;
        RotationSpeed.value = Settings.RotationSpeed;
        SlowDownStrength.value = Settings.SlowDownStrength;
        KeyboardAimingMode.isOn = Settings.KeyboardAimingMode;
        PlayerColor.text = ColorUtility.ToHtmlStringRGB(Settings.PlayerColor);
    }
}
