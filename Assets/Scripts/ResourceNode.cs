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
    float HealthIncrement = 20;

    // other
    public ResourceType Type;
    public bool IsMined;

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
        MinerMaxHealth = HealthIncrement;
        MinerHealth = MinerMaxHealth;
    }

    public void UpgradeMiner()
    {
        MinerLevel++;
        MinerMaxHealth = MinerLevel * HealthIncrement;
        MinerHealth = MinerMaxHealth;
    }

    public void GetResource()
    {
        if (IsMined)
            GameObject.Find("Game Manager").GetComponent<GameManager>().Resources[Type] += ResourceAmount * MinerLevel;
    }

    public void MinerCollisionCheck(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && IsMined)
            if (collision.gameObject.GetComponent<EnemyController>().CurrentState != EnemyController.EnemyState.Dead)
            {
                MinerHealth -= collision.gameObject.GetComponent<EnemyController>().Damage;
                if (MinerHealth <= 0)
                    IsMined = false;

                Destroy(collision.gameObject);
            }
    }
}
