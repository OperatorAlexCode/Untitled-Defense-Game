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
        GameObject.Find("Start").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadGameScene);
        GameObject.Find("Settings").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadSettingsMenuScene);
        GameObject.Find("Exit").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.ExitApplication);
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
