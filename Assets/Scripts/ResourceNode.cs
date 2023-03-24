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
        }
    }

    public void BuildMiner()
    {
        IsMined = true;
    }

    public void UpgradeMiner()
    {

    }

    public void GetResource()
    {

    }

    enum ResourceType
    {
        Gold,
        Gunpowder
    }
}
