using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<ResourceType,int> Resources;
    public int CurrentWave;
    public float PlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        Resources = new Dictionary<ResourceType,int>();

        var resourceNames = Enum.GetNames(typeof(ResourceType));

        foreach (int type in Enum.GetValues(typeof(ResourceType)))
            Resources.Add(Enum.Parse<ResourceType>(resourceNames[type]),0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth <= 0)
        {
            EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }

    public void StartWave()
    {
        CurrentWave++;
        GameObject.Find("Cannon").gameObject.GetComponent<CannonController>().ActivateDeactivate(true);
    }

    public void LoseHealth(float damage)
    {
        PlayerHealth -= damage;
    }
}
