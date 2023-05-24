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
    public GameObject enemy;
    public GameObject giant;
    public GameObject fastEnemy;

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
        
        if (GM.CurrentWave <=5)
        {
            enemySpawnTimerReset = 5;

            if (GM.CurrentWave == 1)
            {
                //Spawns only standard enemies
                smallestRngNumber = 20;
                biggestRngNumber = 80;
            }
            if (GM.CurrentWave == 2)
            {
                //Some big enemies appear
                smallestRngNumber = 20;
                biggestRngNumber = 100;
            }

            if (GM.CurrentWave == 3)
            {
                //Some fast enemies appear
                smallestRngNumber = -20;
                biggestRngNumber = 80;
            }

            if (GM.CurrentWave == 4)
            {
                //Only big enemies appear
                smallestRngNumber = 80;
                biggestRngNumber = 200;
            }

        }

        if (GM.CurrentWave <= 9)
        {
            enemySpawnTimerReset = 4;

            if (GM.CurrentWave == 5)
            {
                //Only fast enemies appear
                smallestRngNumber = 0;
                biggestRngNumber = 20;
            }
        }
        
        if (GM.CurrentWave <=13)
        {
            enemySpawnTimerReset = 3;
        }

        if (GM.CurrentWave <=17)
        {
            enemySpawnTimer = 2;
        }

        if (GM.CurrentWave <=21)
        {
            enemySpawnTimer = 1;
        }
    }

    void SpawnEnemy()
    {
        //Generates a random number to spawn different enemies based on
        float enemySpawnNumber = Random.Range(smallestRngNumber, biggestRngNumber);

        if (enemySpawnNumber < 80 && enemySpawnNumber > 20)
        {
            Transform enemytransform = enemy.transform;
            enemytransform.position = new Vector3(250, 2, Random.Range(-50, 50));
            Instantiate(enemy, enemytransform);
        }
        else if (enemySpawnNumber < 21)
        {
            Transform fastEnemytransform = fastEnemy.transform;
            fastEnemytransform.position = new Vector3(250, 5, Random.Range(-50, 50));
            Instantiate(fastEnemy, fastEnemytransform);
        }

        else if (enemySpawnNumber > 79)
        {
            Transform gianttransform = giant.transform;
            gianttransform.position = new Vector3(250, 5, Random.Range(-50, 50));
            Instantiate(giant, gianttransform);
        }
    }

}
