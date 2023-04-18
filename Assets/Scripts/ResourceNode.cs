using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public int ResourceAmount;
    public int MinerHealth;
    public int MinerMaxHealth;
    int MinerLevel;
    ResourceType Type;
    bool IsMined;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MinerHealth < 0 && IsMined == true)
        {
            IsMined = false;
            MinerLevel = 0;
        }
    }

    public void BuildMiner()
    {
        IsMined = true;
        MinerLevel = 1;
    }

    public void UpgradeMiner()
    {

    }

    public void GetResource()
    {
        GameObject.Find("Game Manager").GetComponent<GameManager>().Resources[Type] += ResourceAmount*MinerLevel;
    }
}
