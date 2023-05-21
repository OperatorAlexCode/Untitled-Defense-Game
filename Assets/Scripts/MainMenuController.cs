using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    void Awake()
    {
        GameObject.Find("Start").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadGameScene);
        GameObject.Find("Settings").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadSettingsMenuScene);
        GameObject.Find("Exit").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.ExitApplication);   
    }
}
