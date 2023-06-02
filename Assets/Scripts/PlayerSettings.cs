using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using Unity.VisualScripting;
using ColorUtility = UnityEngine.ColorUtility;
using UnityEngine.InputSystem;

public class PlayerSettings : MonoBehaviour
{
    // String | float keys
    string MusicVolumeKey = "MusicVolume";
    string SfxVolumeKey = "SfxVolume";
    string MouseSensitivityKey = "MouseSensitivity";
    string RotationSpeedKey = "RotationSpeed";
    string SlowDownStrengthKey = "SlowDownStrength";
    // String | Keycode Keys
    string MovementKeysKey = "MovementKeys";
    string ShotTypeHotKeysKey = "ShotTypeHotKeys";
    string ShotTypesSelectKeysKey = "ShotTypesSelectKeys";
    string FireKeyKey = "FireKey";
    string SlowDownKeyKey = "SlowDownKey";
    string PauseKeyKey = "PauseKey";
    // String | misc keys
    string KeyboardAimingModeKey = "KeyboardAimingMode";
    string PlayerColorKey = "PlayerColor";

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
    public KeyCode PauseKey = KeyCode.Escape;

    // Other
    public Color PlayerColor;
    public bool KeyboardAimingMode = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadSettings();
        SceneController.LoadMainMenuScene();
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
        PlayerPrefs.SetString(PlayerColorKey, ColorUtility.ToHtmlStringRGB(PlayerColor));
        PlayerPrefs.Save();
    }

    public void SetToDefaultSettings()
    {
        RotationSpeed = 40;
        SlowDownStrength = 0.5f;
        MusicVolume = 0.75f;
        SfxVolume = 0.65f;
        MouseSensitivity = 1.0f;
        PlayerColor = new(0.5843138f, 0.2823529f, 0.3647058f);
        KeyboardAimingMode = false;
    }

    public void SaveKeybinds()
    {
        PlayerPrefs.SetString(MovementKeysKey, KeycodeArrayToString(MovementKeys));
        PlayerPrefs.SetString(ShotTypeHotKeysKey, KeycodeArrayToString(ShotTypeHotKeys));
        PlayerPrefs.SetString(ShotTypesSelectKeysKey, KeycodeArrayToString(ShotTypesSelectKeys));
        PlayerPrefs.SetString(FireKeyKey, FireKey.ToString());
        PlayerPrefs.SetString(SlowDownKeyKey, SlowDownKey.ToString());
        PlayerPrefs.SetString(PauseKeyKey, PauseKey.ToString());
        PlayerPrefs.Save();
    }

    public void SetToDefaultKeyBinds()
    {
        MovementKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
        ShotTypeHotKeys = new KeyCode[] { KeyCode.Z, KeyCode.X, KeyCode.C };
        ShotTypesSelectKeys = new KeyCode[] { KeyCode.Q, KeyCode.E };
        FireKey = KeyCode.Space;
        SlowDownKey = KeyCode.LeftShift;
        PauseKey = KeyCode.P;
    }

    public void LoadSettings()
    {
        // Settings
        if (PlayerPrefs.HasKey(MusicVolumeKey))
            MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);

        if (PlayerPrefs.HasKey(SfxVolumeKey))
            SfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey);

        if (PlayerPrefs.HasKey(MouseSensitivityKey))
            MouseSensitivity = PlayerPrefs.GetFloat(MouseSensitivityKey);

        if (PlayerPrefs.HasKey(RotationSpeedKey))
            RotationSpeed = PlayerPrefs.GetFloat(RotationSpeedKey);

        if (PlayerPrefs.HasKey(SlowDownStrengthKey))
            SlowDownStrength = PlayerPrefs.GetFloat(SlowDownStrengthKey);

        // KeyBinds
        if (PlayerPrefs.HasKey(MovementKeysKey))
            MovementKeys = StringToKeyCodeArray(PlayerPrefs.GetString(MovementKeysKey));

        if (PlayerPrefs.HasKey(ShotTypeHotKeysKey))
            ShotTypeHotKeys = StringToKeyCodeArray(PlayerPrefs.GetString(ShotTypeHotKeysKey));

        if (PlayerPrefs.HasKey(ShotTypesSelectKeysKey))
            ShotTypesSelectKeys = StringToKeyCodeArray(PlayerPrefs.GetString(ShotTypesSelectKeysKey));

        if (PlayerPrefs.HasKey(FireKeyKey))
            FireKey = StringToKeyCode(PlayerPrefs.GetString(FireKeyKey));

        if (PlayerPrefs.HasKey(SlowDownKeyKey))
            SlowDownKey = StringToKeyCode(PlayerPrefs.GetString(SlowDownKeyKey));

        if (PlayerPrefs.HasKey(PauseKeyKey))
            PauseKey = StringToKeyCode(PlayerPrefs.GetString(PauseKeyKey));

        // Others
        if (PlayerPrefs.HasKey(KeyboardAimingModeKey))
            KeyboardAimingMode = Convert.ToBoolean(PlayerPrefs.GetInt(KeyboardAimingModeKey));

        if (PlayerPrefs.HasKey(PlayerColorKey))
        {
            Color color;
            if (ColorUtility.TryParseHtmlString(PlayerPrefs.GetString(PlayerColorKey), out color))
                PlayerColor = color;
        }
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
