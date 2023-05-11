using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerSettings : MonoBehaviour
{
    // Float
    public float RotationSpeed = 40;
    public float SlowDownStrength = 0.5f;
    public float MusicVolume = 0.75f;
    public float SfxVolume = 0.65f;
    public float MouseSensitivity = 1.0f;

    // Keycode
    public KeyCode[] MovementKeys = new KeyCode[] {KeyCode.W,KeyCode.S, KeyCode.A, KeyCode.D };
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
        PlayerColor = new(0.5843138f, 0.2823529f, 0.3647058f);
    }
}
