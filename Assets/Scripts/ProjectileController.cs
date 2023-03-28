using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float LifeTime;
    float Age;
    public float Damage;
    public float knockBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Age += Time.deltaTime;

        if (Age > LifeTime)
            Destroy(gameObject);
    }
}
