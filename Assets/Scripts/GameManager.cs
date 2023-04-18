using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<ResourceType,int> Resources;
    public int CurrentWave;

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
        
    }

    public void StartWave()
    {
        CurrentWave++;
    }

}
