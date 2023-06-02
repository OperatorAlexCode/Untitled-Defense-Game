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

    //Variabels for jumping
    public bool canJump;
    public float jumpFrequency = 1;
    public float jumpTimer;
    public float jumpAmount = 500;

    // Start is called before the first frame update
    void Start()
    {
        //Gives the object a rigidbody
        rb = gameObject.GetComponent<Rigidbody>();
        //Gives jumptimer to value of jump frequency
        jumpTimer = jumpFrequency;
    }
    

    // Update is called once per frame
    void Update()
    {
        //Moves the enemy if their not dead
        if (!gameObject.GetComponent<EnemyController>().IsDead())
        {
            //Moves the enemies with the force from enemyspeed
            rb.AddTorque(movementVector * enemySpeed * Time.deltaTime);

            //The code runs if canjump is true and their above the games groundlevel of 5
            if (canJump == true && rb.position.y <= 5)
            {
                //The timer is moved down
                jumpTimer = jumpTimer -= Time.deltaTime;

                //If the timer reach 0 its reset and 
                if (jumpTimer < 0)
                {
                    //Adds a force on the y axis that is equivelent to jumpAmount
                    rb.AddForce(0, jumpAmount, 0);

                    //Resets the timer
                    jumpTimer = jumpFrequency;
                }
            }
        }

        //Caps speed on the x axis to MaxVelocity
        if (rb.velocity.x <= -MaxVelocity)
            rb.velocity = new Vector3 (-MaxVelocity, rb.velocity.y, rb.velocity.z);

        
    }
}
