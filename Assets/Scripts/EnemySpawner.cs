using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using System.Net.Mime;
using System;
using System.Security.Cryptography;
using System.Collections.Specialized;
using Random = UnityEngine.Random;
using System.Net.Http.Headers;

public class EnemySpawner : MonoBehaviour
{
    //Spawns enemies while timer is active
    public float activeWaveTimer = 20;
    //Time Between spawned enemies  (not implemented yet)
    public float enemySpawnTimer = 5;
    float enemySpawnTimerReset = 5;
    int smallestRngNumber = 0;
    int biggestRngNumber = 100;

    public TextMeshProUGUI timerText;

    //Enemies that can be summoned
    public GameObject minion;
    public GameObject giant;
    public GameObject charger;

    //On of switch for enemy spawns
    public bool SpawnEnemies = false;

    public float currentTime;

    public GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnEnemies == true)
        {
            //Countsdown activeWavetimer
            activeWaveTimer = activeWaveTimer -= Time.deltaTime;

            ////Changes the name of the timertext to the value of activeWaveTimer
            //timerText.text = new string("Next Wave in:") + activeWaveTimer.ToString("0");

            //If the timer reches 0
            if (activeWaveTimer <= 0)
            {
                ////Resets the timer
                //activeWaveTimer = 100;
            }
            //If the activetimer is not 0 or lower
            if (activeWaveTimer > 0)
            {
                //Countsdown the enemyspawntimer
                enemySpawnTimer = enemySpawnTimer -= Time.deltaTime;

                if (enemySpawnTimer < 0)
                {
                    {
                        //Resets the timer
                        enemySpawnTimer = enemySpawnTimerReset;
                        
                        //Calls the spawnEnemy function
                        SpawnEnemy();
                    }
                }
            }
        }
    }

    public void StartStopWave(bool value)
    {
        SpawnEnemies = value;
        EnemyWaves();

        if (value == true)
        {
            activeWaveTimer = GM.CurrentWave * 20;
            
            SpawnEnemy();
        }
    }

    void EnemyWaves()
    {
        
        if (GM.CurrentWave < 5)
        {
            enemySpawnTimerReset = 5;

            switch (GM.CurrentWave)
            {
                case 1:
                    //Spawns only standard enemies
                    smallestRngNumber = 20;
                    biggestRngNumber = 80;
                    break;
                case 2:
                    //Some big enemies appear
                    smallestRngNumber = 20;
                    biggestRngNumber = 100;
                    break;
                case 3:
                    //Some fast enemies appear
                    smallestRngNumber = -20;
                    biggestRngNumber = 80;
                    break;
                case 4:
                    //Only big enemies appear
                    smallestRngNumber = 80;
                    biggestRngNumber = 200;
                    break;
            }
        }

        else if (GM.CurrentWave <= 9)
        {
            enemySpawnTimerReset = 4;

            if (GM.CurrentWave == 5)
            {
                //Only fast enemies appear
                smallestRngNumber = 0;
                biggestRngNumber = 20;
            }
        }
        
        else if (GM.CurrentWave <=13)
        {
            enemySpawnTimerReset = 3;
        }

        else if (GM.CurrentWave <=17)
        {
            enemySpawnTimer = 2;
        }

        else if (GM.CurrentWave <=21)
        {
            enemySpawnTimer = 1;
        }
    }

    void SpawnEnemy()
    {
        //Generates a random number to spawn different enemies based on
        float enemySpawnNumber = Random.Range(smallestRngNumber, biggestRngNumber);

        GameObject enemyToSpawn;

        if (enemySpawnNumber < 21)
        {
            enemyToSpawn = charger;
        }

        else if (enemySpawnNumber > 79)
        {
            enemyToSpawn = giant;
        }

        else
            enemyToSpawn = minion;

        GameObject newEnemy = Instantiate(enemyToSpawn);
        newEnemy.transform.position = new Vector3(250, 2, Random.Range(-50, 50));
    }

}
