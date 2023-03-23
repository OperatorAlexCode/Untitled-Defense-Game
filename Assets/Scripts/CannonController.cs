using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class CannonController : MonoBehaviour
{
    // float | Projectile
    public float BallRadius;
    public float RotationSpeed;
    public float CannonBallMass;
    public float CannonBallDrag;

    // float | Cannon
    public float ShotForce;
    public float RotationUpMax;
    public float RotationDownMin;
    public float RotationYMinMax;
    float CurrentAngle;
    float CurrentRotation;

    // GameOvject
    public GameObject Turret;
    public GameObject Barrel;
    public GameObject ShotDir;

    // KeyCode
    public KeyCode LeftTurnKey;
    public KeyCode RightTurnKey;
    public KeyCode UpTurnKey;
    public KeyCode DownTurnKey;
    public KeyCode FireKey;
    KeyCode[] Controls;

    // Vector3
    Vector3 BarrelStartRotation;
    Vector3 TurretStartRotation;

    // Other
    public Material CannonBallMat;
    ShotType CurrentShot;

    // Start is called before the first frame update
    void Start()
    {
        CurrentShot = ShotType.CannonBall;
        Controls = new KeyCode[] { LeftTurnKey, RightTurnKey, UpTurnKey, DownTurnKey, FireKey };
        BarrelStartRotation = Barrel.transform.localEulerAngles;
        TurretStartRotation = Turret.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(FireKey))
        {
            switch (CurrentShot)
            {
                case ShotType.CannonBall:
                    CannonBall();
                    break;
                case ShotType.GrapeShot:
                    GrapeShot(10);
                    break;
            }
        }

        float rotationamount = RotationSpeed * Time.deltaTime;
        Vector3 barrelRotation = Barrel.transform.localEulerAngles;
        Vector3 turretRotation = Turret.transform.localEulerAngles;

        if (Input.GetKey(LeftTurnKey))
        {
            
            if (CurrentRotation + rotationamount > RotationYMinMax)
            {
                turretRotation = new Vector3(turretRotation.x, TurretStartRotation.y + RotationYMinMax, turretRotation.z);
                CurrentRotation = +RotationYMinMax;
            }

            else
            {
                Turret.transform.Rotate(Vector3.back, rotationamount);
                CurrentRotation += rotationamount;
            }
        }

        if (Input.GetKey(RightTurnKey))
        {
            if (CurrentRotation - rotationamount < -RotationYMinMax)
            {
                turretRotation = new Vector3(turretRotation.x,TurretStartRotation.y - RotationYMinMax, turretRotation.z);
                CurrentRotation = -RotationYMinMax;
            }

            else
            {
                Turret.transform.Rotate(Vector3.forward, rotationamount);
                CurrentRotation -= rotationamount;
            }
        }

        if (Input.GetKey(UpTurnKey))
        {
            if (CurrentAngle + rotationamount > RotationUpMax)
            {
                barrelRotation = new Vector3(BarrelStartRotation.x + RotationUpMax, barrelRotation.y, barrelRotation.z);
                CurrentAngle = BarrelStartRotation.x + RotationUpMax;
            }

            else
            {
                Barrel.transform.Rotate(Vector3.right, rotationamount);
                CurrentAngle += rotationamount;
            }
        }

        if (Input.GetKey(DownTurnKey))
        {
            if (CurrentAngle - rotationamount < RotationDownMin)
            {
                barrelRotation = new Vector3(BarrelStartRotation.x - RotationDownMin, barrelRotation.y, barrelRotation.z);
                CurrentAngle = RotationDownMin;
            }   

            else
            {
                Barrel.transform.Rotate(Vector3.left, rotationamount);
                CurrentAngle -= rotationamount;
            }
               
        }
    }

    void CannonBall()
    {
        GameObject newBall = CreateProjectile("Cannon Ball", ShotDir.transform.position, CannonBallMat, BallRadius, CannonBallMass, CannonBallDrag);

        newBall.SetActive(true);
        newBall.GetComponent<Rigidbody>().AddForce(ShotDir.transform.forward * ShotForce, ForceMode.Impulse);
    }

    void GrapeShot(int shotAmount)
    {
        List<GameObject> totalShot = new();
        float radius = BallRadius / (shotAmount * 1.5f);
        for (int x = 0; x < shotAmount; x++)
        {
            Vector3 pos = new Vector3(ShotDir.transform.position.x, ShotDir.transform.position.y + Random.Range(-0.25f + radius, 0.25f - radius), ShotDir.transform.position.z + Random.Range(-0.25f + radius, 0.25f - radius));
            GameObject newShot = CreateProjectile("Shot", pos, CannonBallMat, radius, CannonBallMass / shotAmount, CannonBallDrag / shotAmount);
            totalShot.Add(newShot);
        }

        foreach (GameObject shot in totalShot)
        {
            shot.SetActive(true);
            shot.GetComponent<Rigidbody>().AddForce((ShotDir.transform.position - Barrel
                .transform.position).normalized * (ShotForce / (shotAmount * 1.5f)), ForceMode.Impulse);
        }
    }

    // Creates a sphere with a given name, position, material, collider, radius nad rigidody with a specified mass and drag
    GameObject CreateProjectile(string name, Vector3 pos, Material mat, float radius, float mass, float drag)
    {
        GameObject cannonBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        cannonBall.name = name;
        cannonBall.transform.position = pos;
        cannonBall.GetComponent<MeshRenderer>().material = mat;

        cannonBall.AddComponent<SphereCollider>();
        cannonBall.GetComponent<SphereCollider>().radius = radius;

        cannonBall.AddComponent<Rigidbody>();
        Rigidbody rigidBody = cannonBall.GetComponent<Rigidbody>();
        rigidBody.mass = mass;
        rigidBody.drag = drag;

        return cannonBall;
    }
}

public enum ShotType
{
    CannonBall,
    GrapeShot,
    Railgun
}
