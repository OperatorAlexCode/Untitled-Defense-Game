using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // float
    public float Health;
    public float Damage;
    public float Speed;
    public float AttackCooldown;
    public float despawnTimer = 10;
    //If true decrease the value instead
    public bool countDown = true;

    // Other
    public EnemyState CurrentState;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = EnemyState.Moving;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy logic
        if (CurrentState != EnemyState.Dead)
        {

        }

        //Trickles down despawntimer if the enemies are dead
        if (CurrentState == EnemyState.Dead)
        {
            despawnTimer = countDown ? despawnTimer -= Time.deltaTime : despawnTimer += Time.deltaTime;
            if (despawnTimer < 0)
            {
                //Despawn Enemy
                Destroy(gameObject);
            }
        }
       
    }

    public void Hurt(float damage, float knockback, Vector3 projectilePos)
    {
        Health -= damage;

        Vector3 knockbackVector = (transform.position - projectilePos).normalized * knockback;

        if (Health <= 0)
        {
            CurrentState = EnemyState.Dead;
            knockbackVector *= knockback;
        }

        GetComponent<Rigidbody>().AddForce(knockbackVector, ForceMode.Impulse);
    }

    //If the enemy collides with an object:
    void onCollisionEnter(Collision collisionInfo)
    {
        //If it collides with a wall:
        if (collisionInfo.collider.name == "Wall")
        {
            CurrentState = EnemyState.Dead;
        }
    }

    public bool IsDead()
    {
        return CurrentState == EnemyState.Dead;
    }

    public enum EnemyState
    {
        Moving,
        Attacking,
        Stunned,
        Dead
    }
}
