using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // float
    public float Health;
    public float Damage;
    public float AttackCooldown;
    public float despawnTimer = 10;
    public float knockbackResistance;
    //If true decrease the value instead
    public bool countDown = true;

    // Other
    public EnemyState CurrentState;
    public AudioClip DeathSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = EnemyState.Moving;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = GameObject.Find("PlayerSettings").gameObject.GetComponent<PlayerSettings>().SfxVolume;
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

        Vector3 knockbackVector = (transform.position - projectilePos).normalized * knockback / knockbackResistance;

        if (Health <= 0)
        {
            audioSource.clip = DeathSound;
            CurrentState = EnemyState.Dead;
            knockbackVector *= knockback;
        }
        
        GetComponent<Rigidbody>().AddForce(knockbackVector, ForceMode.Impulse);
        audioSource.Play();
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
