using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Gives acess to rigidbody
    Rigidbody rb;

    //Enemy vector used for movement
    public Vector3 movementVector;
    //Float variabels
    public float MaxVelocity;
    public float enemySpeed;
    public float MaxRotation;

    //Vectors for jumping
    public bool canJump;
    public float jumpFrequency = 2;
    public float jumpTimer;
    public float jumpAmount = 1000;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        jumpTimer = jumpFrequency;
    }
    

    // Update is called once per frame
    void Update()
    {
        //Moves the enemy if their not dead
        if (!gameObject.GetComponent<EnemyController>().IsDead())
            rb.AddTorque(movementVector * enemySpeed * Time.deltaTime);

        //Caps speed on the x axis to MaxVelocity
        if (rb.velocity.x <= -MaxVelocity)
            rb.velocity = new Vector3 (-MaxVelocity, rb.velocity.y, rb.velocity.z);

        //The code runs if canjump is true and their above the games groundlevel of 5
        if (canJump == true && rb.position.y <= 5)
        {
            //The timer is moved down
            jumpTimer = jumpTimer -= Time.deltaTime;

            //If the timer reach 0 its reset and 
            if (jumpTimer < 0)
            {
                rb.AddForce(0, jumpAmount, 0);

                //Resets the timer
                jumpTimer = jumpFrequency;
            }
        }
    }
}
