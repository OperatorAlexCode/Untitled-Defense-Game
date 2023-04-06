using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Gives acess to rigidbody
    public Rigidbody rb;
    
    //Position variabels
    public float enemyX = 300;
    public float enemyY = 5;
    public float enemyZ = 0;

    //Movement variabels
    public float forwardMovement = 1000;
    public float uppwardMovement = 0;
    public float sidewardMovement = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Sets the enemy position to a new vector

        //Updates the players position with the values
        rb.AddForce(-forwardMovement * Time.deltaTime, uppwardMovement, sidewardMovement);

    }
}
