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

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
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

        //if (rb.velocity.x >= MaxVelocity)
        //    rb.velocity = new Vector3(MaxVelocity, rb.velocity.y, rb.velocity.z);

        ////Dont work
        //if (rb.rotation.x >= MaxRotation || rb.rotation.x <= -MaxRotation)
        //    rb.rotation.eulerAngles = new Vector3();

        //Debug.Log(rb.velocity);
    }
}
