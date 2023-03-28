using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Health;
    public float Damage;
    public float Speed;
    public float AttackCooldown;

    EnemyState CurrentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy logic
        if (CurrentState != EnemyState.Dead)
        {

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

    enum EnemyState
    {
        Moving,
        Attacking,
        Stunned,
        Dead
    }
}
