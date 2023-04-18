using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    // int
    public int ResourceAmount;
    int MinerLevel;

    //float
    public float MinerHealth;
    public float MinerMaxHealth;

    // other
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
        MinerHealth = 10;
    }

    public void UpgradeMiner()
    {
        MinerLevel++;
        MinerHealth = MinerLevel * 10;
    }

    public void GetResource()
    {
        GameObject.Find("Game Manager").GetComponent<GameManager>().Resources[Type] += ResourceAmount*MinerLevel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && IsMined)
        {
            MinerHealth -= collision.gameObject.GetComponent<EnemyController>().Damage;
            if (MinerHealth <= 0)
                IsMined = false;

            Destroy(collision.gameObject);
        }
    }
}
