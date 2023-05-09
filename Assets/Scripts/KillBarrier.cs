using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBarrier : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            //collision.gameObject.GetComponent<EnemyController>().Hurt(float.PositiveInfinity,0,collision.gameObject.transform.position);
            Destroy(collision.gameObject);
    }
}
