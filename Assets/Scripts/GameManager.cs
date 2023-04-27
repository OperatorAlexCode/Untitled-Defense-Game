using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<ResourceType,int> Resources;
    public int CurrentWave;
    public float PlayerHealth;
    public bool InWave;
    public EnemySpawner SpawnManager;
    public GameObject BuildUI;
    public List<ResourceNode> Nodes;
    public int PassiveGoldIncome;

    // Start is called before the first frame update
    void Start()
    {
        Resources = new Dictionary<ResourceType,int>();

        var resourceNames = Enum.GetNames(typeof(ResourceType));

        foreach (int type in Enum.GetValues(typeof(ResourceType)))
            Resources.Add(Enum.Parse<ResourceType>(resourceNames[type]),0);

        Resources[ResourceType.gold] = 200;

        GameObject.Find("DayNightManager").gameObject.GetComponent<WaveCycle>().TurnSunOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth <= 0)
        {
            EditorApplication.isPlaying = false;
            Application.Quit();
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (InWave && (enemies.All(e => e.GetComponent<EnemyController>().CurrentState == EnemyController.EnemyState.Dead) || enemies.Length == 0) && SpawnManager.activeWaveTimer <= 0)
            StopWave();
    }

    public void StartWave()
    {
        CurrentWave++;
        GameObject.Find("Cannon").gameObject.GetComponent<CannonController>().ActivateDeactivate(true);
        GameObject.Find("SpawnManager").gameObject.GetComponent<EnemySpawner>().StartStopWave(true);
        GameObject.Find("DayNightManager").gameObject.GetComponent<WaveCycle>().TurnSunOn();
        InWave = true;
        BuildUI.SetActive(false);
    }

    public void StopWave()
    {
        GameObject.Find("Cannon").gameObject.GetComponent<CannonController>().ActivateDeactivate(false);
        GameObject.Find("SpawnManager").gameObject.GetComponent<EnemySpawner>().StartStopWave(false);
        GameObject.Find("DayNightManager").gameObject.GetComponent<WaveCycle>().TurnSunOff();
        InWave = false;

        BuildUI.SetActive(true);

        foreach (ResourceNode rn in Nodes)
            rn.GetResource();

        Resources[ResourceType.gold] += PassiveGoldIncome;
    }

    public void LoseHealth(float damage)
    {
        PlayerHealth -= damage;
    }
}
