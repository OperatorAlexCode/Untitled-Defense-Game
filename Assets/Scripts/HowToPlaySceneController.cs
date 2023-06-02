using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlaySceneController : MonoBehaviour
{
    void Awake()
    {
        GameObject.Find("Go Back").GetComponent<Button>().onClick.AddListener(SceneController.LoadMainMenuScene);
    }
}
