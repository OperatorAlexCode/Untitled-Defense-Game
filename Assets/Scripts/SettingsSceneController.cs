using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSceneController : MonoBehaviour
{
    private void Awake()
    {
        GameObject.Find("MainMenu").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadMainMenuScene);
    }
}
