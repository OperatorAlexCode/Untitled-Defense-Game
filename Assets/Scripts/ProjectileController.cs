using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    float Age;
    float DeathDestroyDelay = 0.5f;
    public float LifeTime;
    public float Damage;
    public float knockBack;
    bool HasDoneDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Age > LifeTime)
            Destroy(gameObject);
        else
            Age += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && !HasDoneDamage)
        {
            collision.gameObject.GetComponent<EnemyController>().Hurt(Damage, knockBack, transform.position, GetComponent<Rigidbody>().velocity);
            LifeTime = DeathDestroyDelay;
            Age = 0;
            HasDoneDamage = true;
        }

        else
        {
            LifeTime = 0.1f;
            Age = 0;
        }
    }
}
