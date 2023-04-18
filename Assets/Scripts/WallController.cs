using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject enemy = collision.gameObject;
            GameObject.Find("Game Manager").GetComponent<GameManager>().LoseHealth(enemy.GetComponent<EnemyController>().Damage);
            Destroy(collision.gameObject);
        }
    }
}
