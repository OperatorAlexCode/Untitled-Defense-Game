using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // int
    public int Damage;
    public int knockBack;

    // float
    public float LifeTime;
    float Age;

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
