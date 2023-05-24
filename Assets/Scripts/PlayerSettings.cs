using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using Unity.VisualScripting;

public class PlayerSettings : MonoBehaviour
{
    // String
    string MusicVolumeKey = "MusicVolume";
    string SfxVolumeKey = "SfxVolume";
    string MouseSensitivityKey = "MouseSensitivity";
    string RotationSpeedKey = "RotationSpeed";
    string SlowDownStrengthKey = "SlowDownStrength";
    string PlayerColorKey = "PlayerColor";
    string KeyboardAimingModeKey = "KeyboardAimingMode";

    // Float
    public float RotationSpeed = 40;
    public float SlowDownStrength = 0.5f;
    public float MusicVolume = 0.75f;
    public float SfxVolume = 0.65f;
    public float MouseSensitivity = 1.0f;

    // Keycode
    public KeyCode[] MovementKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
    public KeyCode[] ShotTypeHotKeys = new KeyCode[] { KeyCode.Z, KeyCode.X, KeyCode.C };
    public KeyCode[] ShotTypesSelectKeys = new KeyCode[] { KeyCode.Q, KeyCode.E };
    public KeyCode FireKey = KeyCode.Space;
    public KeyCode SlowDownKey = KeyCode.LeftShift;
    public KeyCode PauseKey = KeyCode.P;

    // Other
    public Color PlayerColor;
    public bool KeyboardAimingMode = true;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadSettings();
        SceneController.LoadMainMenuScene();
    }

    public void SetToDefaultSettings()
    {
        RotationSpeed = 40;
        SlowDownStrength = 0.5f;
        MusicVolume = 0.75f;
        SfxVolume = 0.65f;
        MouseSensitivity = 1.0f;

        MovementKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
        ShotTypeHotKeys = new KeyCode[] { KeyCode.Z, KeyCode.X, KeyCode.C };
        ShotTypesSelectKeys = new KeyCode[] { KeyCode.Q, KeyCode.E };
        FireKey = KeyCode.Space;
        SlowDownKey = KeyCode.LeftShift;
        PauseKey = KeyCode.P;
        PlayerColor = new(0.5843138f, 0.2823529f, 0.3647058f);
        KeyboardAimingMode = true;
    }

    /// <summary>
    /// Save setting Values to playerprefs
    /// </summary>
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, MusicVolume);
        PlayerPrefs.SetFloat(SfxVolumeKey, SfxVolume);
        PlayerPrefs.SetFloat(MouseSensitivityKey, MouseSensitivity);
        PlayerPrefs.SetFloat(RotationSpeedKey, RotationSpeed);
        PlayerPrefs.SetFloat(SlowDownStrengthKey, SlowDownStrength);
        PlayerPrefs.SetInt(KeyboardAimingModeKey, Convert.ToInt32(KeyboardAimingMode));
        PlayerPrefs.SetString(PlayerColorKey, UnityEngine.ColorUtility.ToHtmlStringRGB(PlayerColor));
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {

    }

    string KeycodeArrayToString(KeyCode[] array)
    {
        return string.Join(",", array);
    }

    KeyCode StringToKeyCode(string str)
    {
        return (KeyCode)Enum.Parse(typeof(KeyCode), str);
    }

    KeyCode[] StringToKeyCodeArray(string str)
    {
        string[] keys = str.Split(",");

        KeyCode[] output = new KeyCode[keys.Length];

        for (int x = 0; x < output.Length; x++)
            output[x] = (KeyCode)Enum.Parse(typeof(KeyCode), keys[x]);

        return output;
    }
}
