using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI BestWaveText;
    [SerializeField]
    GameObject PersonalBest;

    void Awake()
    {
        // Add a listener to each onclick event for the scene it shall go to
        GameObject.Find("Start").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadGameScene);
        GameObject.Find("HowToPlay").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadHowToPlayScene);
        GameObject.Find("Settings").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadSettingsMenuScene);
        GameObject.Find("Exit").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.ExitApplication);

        // If there is a personal best key and it's more than 0 it will display it
        if (PlayerPrefs.HasKey("PersonalBest"))
            if (PlayerPrefs.GetInt("PersonalBest") > 0)
                ShowBestWave();
    }

    void ShowBestWave()
    {
        PersonalBest.SetActive(true);
        BestWaveText.text = $"{PlayerPrefs.GetInt("PersonalBest")}";
    }
}
