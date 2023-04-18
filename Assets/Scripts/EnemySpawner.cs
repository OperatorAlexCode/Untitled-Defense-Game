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
    public float activeWaveTimer = 100;
    //Time Between spawned enemies  (not implemented yet)
    public float enemySpawnTimer = 5;

    public TextMeshProUGUI timerText;

    public GameObject enemy;

    //On of switch for enemy spawns
    public bool SpawnEnemies = true;

    public float currentTime;
    //If true decrease the value instead
    public bool countDown = true;

    //float temp = new Random().Next(0, 10);

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
            activeWaveTimer = countDown ? activeWaveTimer -= Time.deltaTime : activeWaveTimer += Time.deltaTime;

            //Changes the name of the timertext to the value of activeWaveTimer
            timerText.text = new string("Next Wave in:") + activeWaveTimer.ToString("0");

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
                enemySpawnTimer = countDown ? enemySpawnTimer -= Time.deltaTime : enemySpawnTimer += Time.deltaTime;

                if (enemySpawnTimer < 0)
                {
                    {
                        //Resets the timer
                        enemySpawnTimer = 5;

                        //Calls the spawnEnemy function
                        SpawnEnemy();
                    }
                }
            }
        }
    }

    public void StartStopWave(bool value)
    {
        SpawnEnemies= value;
    }

    void SpawnEnemy()
    {
        Transform enemytransform = enemy.transform;
        enemytransform.position = new Vector3(300, 2, Random.Range(-30, 50));

        Instantiate(enemy, enemytransform);


    }

    //void GameObject CreateEnemy()
    //{
    //    GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Cube);

    //    enemy.name = name;
    //    enemy.transform.position = position;

    //}
}
