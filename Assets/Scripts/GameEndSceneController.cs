using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndSceneController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI WaveReachedText;

    private void Awake()
    {
        Cursor.visible = true;
        GameObject.Find("Retry btn").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadGameScene);
        GameObject.Find("Main Menu").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadMainMenuScene);
        GameObject.Find("Exit Btn").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.ExitApplication);

        WaveReachedText.text = "Wave Reached: " + PlayerPrefs.GetInt("LastBeatWave").ToString();

        if (PlayerPrefs.HasKey("PersonalBest"))
        {
            if (PlayerPrefs.GetInt("LastBeatWave") > PlayerPrefs.GetInt("PersonalBest"))
                PlayerPrefs.SetInt("PersonalBest", PlayerPrefs.GetInt("LastBeatWave"));
        }
        else
            PlayerPrefs.SetInt("PersonalBest", PlayerPrefs.GetInt("LastBeatWave"));
    }
}
