using Palmmedia.ReportGenerator.Core;
using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        GameObject.Find("MainMenu").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadMainMenuScene);
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
    }

    void UpdateSettingsScene()
    {
        MusicVolume.value = Settings.MusicVolume;
        SfxVolume.value = Settings.SfxVolume;
        MouseSensitivity.value = Settings.MouseSensitivity;
        RotationSpeed.value = Settings.RotationSpeed;
        SlowDownStrength.value = Settings.SlowDownStrength;
        KeyboardAimingMode.isOn = Settings.KeyboardAimingMode;
    }
}
