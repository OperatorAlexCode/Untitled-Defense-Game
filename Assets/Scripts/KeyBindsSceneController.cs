using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using Debug = UnityEngine.Debug;
using Input = UnityEngine.Input;

public class KeyBindsSceneController : MonoBehaviour
{
    // GameObject
    [SerializeField]
    GameObject KeyBindItem;
    [SerializeField]
    GameObject KeyList;

    // Other
    PlayerSettings Settings;
    List<KeyCode> Keys = new();
    int? KeyToRebind = null;

    private void Awake()
    {
        GameObject.Find("GoBack").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadSettingsMenuScene);

        Settings = GameObject.Find("PlayerSettings").gameObject.GetComponent<PlayerSettings>();

        // Gets the keybinds from playersettings and instantiates a KeyListItem for each action
        Keys.AddRange(Settings.MovementKeys);
        Keys.AddRange(Settings.ShotTypeHotKeys);
        Keys.AddRange(Settings.ShotTypesSelectKeys);
        Keys.AddRange(new List<KeyCode> { Settings.FireKey, Settings.SlowDownKey, Settings.PauseKey });

        for (int x = 0; x < Keys.Count; x++)
        {
            int index;
            index = x;
            GameObject go = Instantiate(KeyBindItem);
            go.transform.SetParent(KeyList.transform);
            go.transform.localScale = Vector3.one;

            string functionName = string.Empty;
            switch (x)
            {
                case 0:
                    functionName = "Angle Cannon Up";
                    break;
                case 1:
                    functionName = "Angle Cannon Down";
                    break;
                case 2:
                    functionName = "Rotate Cannon Left";
                    break;
                case 3:
                    functionName = "Rotate Cannon Right";
                    break;
                case 4:
                    functionName = "Select Cannon Ball";
                    break;
                case 5:
                    functionName = "Select Grape Shot";
                    break;
                case 6:
                    functionName = "Select Rail Gun";
                    break;
                case 7:
                    functionName = "Change Shot (Forward)";
                    break;
                case 8:
                    functionName = "Change Shot (Backward)";
                    break;
                case 9:
                    functionName = "Fire Cannon";
                    break;
                case 10:
                    functionName = "Slow Down Cannon";
                    break;
                case 11:
                    functionName = "Pause Game";
                    break;
            }

            // Sets the text for the butten to show the currently bound button and the keyname for what the button does
            go.transform.Find("KeyName").GetComponent<TextMeshProUGUI>().text = functionName;
            go.transform.Find("Button/KeyName").GetComponent<TextMeshProUGUI>().text = Keys[x].ToString();

            go.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => { StartRebindFor(index); });
        }
    }

    private void Update()
    {
        if (KeyToRebind.HasValue)
        {
            if (Input.anyKeyDown)
            {
                KeyCode[] keyCodes = (KeyCode[])Enum.GetValues(typeof(KeyCode));

                foreach (KeyCode key in keyCodes)
                    if (Input.GetKeyDown(key))
                    {
                        if (key != KeyCode.Escape && !Keys.Contains(key))
                        {
                            Keys[KeyToRebind.Value] = key;
                            GetKeyListItem(KeyToRebind.Value).transform.Find("Button/KeyName").GetComponent<TextMeshProUGUI>().text = Keys[KeyToRebind.Value].ToString();
                        }

                        GetKeyListItem(KeyToRebind.Value).transform.Find("Button").GetComponent<Button>().interactable = true;
                        KeyToRebind = null;
                        break;
                    }
            }
        }
    }

    // Sets The keys in playersettings and saves them
    public void SaveKeyBinds()
    {
        for (int x = 0; x < Keys.Count; x++)
        {
            if (x < 4)
                Settings.MovementKeys[x] = Keys[x];
            else if (x < 7)
                Settings.ShotTypeHotKeys[x - (Settings.ShotTypeHotKeys.Length + 1)] = Keys[x];
            else if (x < 9)
                Settings.ShotTypesSelectKeys[x - (Settings.ShotTypeHotKeys.Length + Settings.ShotTypesSelectKeys.Length + 2)] = Keys[x];
            else
            {
                switch (x)
                {
                    case 9:
                        Settings.FireKey = Keys[x];
                        break;
                    case 10:
                        Settings.SlowDownKey = Keys[x];
                        break;
                    case 11:
                        Settings.PauseKey = Keys[x];
                        break;
                }
            }
        }

        Settings.SaveKeybinds();
    }

    public void ResetDefaultBinds()
    {
        Settings.SetToDefaultKeyBinds();
        UpdateKeyBindScreen();
    }

    public void StartRebindFor(int index)
    {
        if (!KeyToRebind.HasValue)
        {
            GetKeyListItem(index).transform.Find("Button").GetComponent<Button>().interactable = false;
            KeyToRebind = index;
        } 
    }

    void UpdateKeyBindScreen()
    {
        Keys.Clear();
        Keys.AddRange(Settings.MovementKeys);
        Keys.AddRange(Settings.ShotTypeHotKeys);
        Keys.AddRange(Settings.ShotTypesSelectKeys);
        Keys.AddRange(new List<KeyCode> { Settings.FireKey, Settings.SlowDownKey, Settings.PauseKey });

        for (int x = 0; x < Keys.Count; x++)
        {
            GetKeyListItem(x).transform.Find("Button/KeyName").GetComponent<TextMeshProUGUI>().text = Keys[x].ToString();
        }

    }

    GameObject GetKeyListItem(int index)
    {
        return KeyList.transform.GetChild(index).gameObject;
    }
}
