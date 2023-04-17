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
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().Hurt(Damage, knockBack, transform.position);
            LifeTime = DeathDestroyDelay;
            Age = 0;
        }
    }
}
