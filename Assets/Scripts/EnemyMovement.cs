using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Gives acess to rigidbody
    Rigidbody rb;

    //Position variabels
    public float enemyX = 300;
    public float enemyY = 5;
    public float enemyZ = 0;

    //Movement variabels
    public float forwardMovement = 1000;
    public float uppwardMovement = 0;
    public float sidewardMovement = 0;
    public float MaxVel;
    public float MaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Sets the enemy position to a new vector

        if (!gameObject.GetComponent<EnemyController>().IsDead())
            rb.AddTorque(-forwardMovement * Time.deltaTime, uppwardMovement, sidewardMovement);

    }
}
