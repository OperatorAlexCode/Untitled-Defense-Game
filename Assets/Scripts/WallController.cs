using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = GameObject.Find("PlayerSettings").gameObject.GetComponent<PlayerSettings>().SfxVolume;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            if (collision.gameObject.GetComponent<EnemyController>().CurrentState != EnemyController.EnemyState.Dead)
            {
                GameObject enemy = collision.gameObject;
                GameObject.Find("Game Manager").GetComponent<GameManager>().LoseHealth(enemy.GetComponent<EnemyController>().Damage);
                Destroy(collision.gameObject);
            }
    }
}
