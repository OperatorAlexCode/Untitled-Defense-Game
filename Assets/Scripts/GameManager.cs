using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Int
    public int CurrentWave;
    public int PassiveGoldIncome;

    // Bool
    public bool InWave;
    public bool Paused;

    // Other
    public Dictionary<ResourceType,int> Resources;
    public float PlayerHealth;
    public EnemySpawner SpawnManager;
    public List<ResourceNode> Nodes;
    public Material[] PlayerMats;
    public PlayerSettings Settings;


    // Start is called before the first frame update
    void Awake()
    {
        Resources = new Dictionary<ResourceType,int>();

        var resourceNames = Enum.GetNames(typeof(ResourceType));

        foreach (int type in Enum.GetValues(typeof(ResourceType)))
            Resources.Add(Enum.Parse<ResourceType>(resourceNames[type]),0);

        Resources[ResourceType.gold] = 200;

        GameObject.Find("DayNightManager").gameObject.GetComponent<WaveCycle>().TurnSunOff();

        GameObject.Find("Main Menu").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.LoadMainMenuScene);
        GameObject.Find("Exit").gameObject.GetComponent<Button>().onClick.AddListener(SceneController.ExitApplication);
        GameObject.Find("Pause Menu").gameObject.SetActive(false);

        Settings = GameObject.Find("PlayerSettings").gameObject.GetComponent<PlayerSettings>();

        foreach (Material mat in PlayerMats)
            mat.color = Settings.PlayerColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth <= 0)
            SceneController.LoadGameOverScene();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (InWave && (enemies.All(e => e.GetComponent<EnemyController>().CurrentState == EnemyController.EnemyState.Dead) || enemies.Length == 0) && SpawnManager.activeWaveTimer <= 0)
            StopWave();

        if (Input.GetKeyDown(Settings.PauseKey))
            Pause();
    }

    public void StartWave()
    {
        CurrentWave++;
        GameObject.Find("Cannon").gameObject.GetComponent<CannonController>().ActivateDeactivate(true);
        GameObject.Find("DayNightManager").gameObject.GetComponent<WaveCycle>().TurnSunOn();
        GameObject.Find("UI Manager").gameObject.GetComponent<UIManager>().ActivateCannonHud();
        GameObject.Find("Music Manager").gameObject.GetComponent<MusicManager>().PlayDayTheme();
        SpawnManager.StartStopWave(true);
        InWave = true;
        Cursor.visible = false;
    }

    public void StopWave()
    {
        SpawnManager.StartStopWave(false);
        GameObject.Find("Cannon").gameObject.GetComponent<CannonController>().ActivateDeactivate(false);
        GameObject.Find("Cannon").gameObject.GetComponent<CannonController>().RestockAmmo((int)ShotType.CannonBall);
        GameObject.Find("DayNightManager").gameObject.GetComponent<WaveCycle>().TurnSunOff();
        GameObject.Find("UI Manager").gameObject.GetComponent<UIManager>().ActivateBuildUI();
        InWave = false;

        foreach (ResourceNode rn in Nodes)
            rn.GetResource();

        Resources[ResourceType.gold] += PassiveGoldIncome;
        GameObject.Find("Music Manager").gameObject.GetComponent<MusicManager>().PlayNightTheme();
        Cursor.visible = true;
    }

    public void LoseHealth(float damage)
    {
        PlayerHealth -= damage;
    }

    public void Pause()
    {
        Paused = !Paused;

        if (Paused)
        {
            Time.timeScale = 0;
            GameObject.Find("UI Manager").gameObject.GetComponent<UIManager>().ShowPauseMenu();
        }
        else
        {
            Time.timeScale = 1;
            GameObject.Find("UI Manager").gameObject.GetComponent<UIManager>().HidePauseMenu();
        } 
    }
}
