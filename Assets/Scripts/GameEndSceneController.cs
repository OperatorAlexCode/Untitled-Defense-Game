using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndSceneController : MonoBehaviour
{
    private void Awake()
    {
        GameObject.Find("Retry btn").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadGameScene);
        GameObject.Find("Exit btn").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.ExitApplication);
    }
}
