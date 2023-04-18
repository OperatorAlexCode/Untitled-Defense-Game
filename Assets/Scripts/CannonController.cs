using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using Random = UnityEngine.Random;

public class CannonController : MonoBehaviour
{
    // float | Projectile
    public float[] ProjectileRadius;
    public float[] ProjectileMass;
    public float[] ProjectileDrag;
    public float[] ProjectileLifetime;
    public float[] ShotForces;
    public float[] MaxCooldowns;
    float[] Cooldowns;
    public float[] Damage;
    public float[] KnockBack;

    // float | Cannon
    public float RotationSpeed;
    public float RotationUpMax;
    public float RotationDownMin;
    public float RotationYMinMax;
    float CurrentAngle;
    float CurrentRotation;

    // Int
    public int UpgradeCostIncrement;
    int[] DamageUpgradeLevel;
    int[] KnockBackUpgradeLevel;
    public int[] ReserveAmmo;

    // GameObject
    public GameObject Turret;
    public GameObject Barrel;
    public GameObject ShotDir;

    // KeyCode
    public KeyCode[] MovementKeys;
    public KeyCode[] ShotTypeHotKeys;
    public KeyCode[] ShotTypeSelectKeys;
    public KeyCode FireKey;
    List<KeyCode> Controls;

    // Vector3
    Vector3 BarrelStartRotation;
    Vector3 TurretStartRotation;

    // Other
    public Material CannonBallMat;
    ShotType CurrentShot;
    bool Active;

    // Start is called before the first frame update
    void Start()
    {
        CurrentShot = ShotType.CannonBall;

        Controls = new List<KeyCode>() { FireKey };
        Controls.AddRange(MovementKeys);
        Controls.AddRange(ShotTypeHotKeys);
        Controls.AddRange(ShotTypeSelectKeys);

        BarrelStartRotation = Barrel.transform.localEulerAngles;
        TurretStartRotation = Turret.transform.eulerAngles;

        Cooldowns = new float[MaxCooldowns.Length];
        DamageUpgradeLevel = new int[] { 1, 1 };
        KnockBackUpgradeLevel = new int[] { 1, 1 };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && Active)
            if (AreAnyKeysPressed(Controls.ToArray(), true))
            {
                #region Shoot cannon with current shot type
                if (IsKeyPressed(FireKey))
                {
                    int index = (int)CurrentShot;

                    if (ReserveAmmo[index] > 0 && Cooldowns[index] <= 0)
                    {
                        switch (CurrentShot)
                        {
                            case ShotType.CannonBall:
                                CannonBall();
                                break;
                            case ShotType.GrapeShot:
                                GrapeShot(5);
                                break;
                        }

                        ReserveAmmo[index] -= 1;
                        Cooldowns[index] = MaxCooldowns[index];
                    }
                }
                #endregion

                #region Moving Turret
                float rotationamount = RotationSpeed * Time.deltaTime;
                Vector3 barrelRotation = Barrel.transform.localEulerAngles;
                Vector3 turretRotation = Turret.transform.localEulerAngles;

                // Rotate Turret Left
                if (IsKeyPressed(MovementKeys[2], true) && !IsKeyPressed(MovementKeys[3], true))
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

                // Rotate Turret Right
                if (IsKeyPressed(MovementKeys[3], true) && !IsKeyPressed(MovementKeys[2], true))
                {
                    if (CurrentRotation - rotationamount < -RotationYMinMax)
                    {
                        turretRotation = new Vector3(turretRotation.x, TurretStartRotation.y - RotationYMinMax, turretRotation.z);
                        CurrentRotation = -RotationYMinMax;
                    }

                    else
                    {
                        Turret.transform.Rotate(Vector3.forward, rotationamount);
                        CurrentRotation -= rotationamount;
                    }
                }

                // Angle Barrel Up
                if (IsKeyPressed(MovementKeys[0], true) && !IsKeyPressed(MovementKeys[1], true))
                {
                    if (CurrentAngle + rotationamount > RotationUpMax && CurrentAngle! >= RotationUpMax)
                    {
                        barrelRotation = new Vector3(BarrelStartRotation.x + RotationUpMax, barrelRotation.y, barrelRotation.z);
                        CurrentAngle = RotationUpMax;
                    }

                    else
                    {
                        Barrel.transform.Rotate(Vector3.right, rotationamount);
                        CurrentAngle += rotationamount;
                    }
                }

                // Angle Barrel Down
                if (IsKeyPressed(MovementKeys[1], true) && !IsKeyPressed(MovementKeys[0], true))
                {
                    if (CurrentAngle - rotationamount < -RotationDownMin && CurrentAngle! <= -RotationDownMin)
                    {
                        barrelRotation = new Vector3(BarrelStartRotation.x - RotationDownMin, barrelRotation.y, barrelRotation.z);
                        CurrentAngle = -RotationDownMin;
                    }

                    else
                    {
                        Barrel.transform.Rotate(Vector3.left, rotationamount);
                        CurrentAngle -= rotationamount;
                    }

                }
                #endregion

                #region Change shot Type
                if (AreAnyKeysPressed(ShotTypeSelectKeys))
                {
                    if (IsKeyPressed(ShotTypeSelectKeys[0]))
                    {
                        if (CurrentShot == 0)
                            CurrentShot = (ShotType)Enum.GetValues(typeof(ShotType)).Length - 1;
                        else
                            CurrentShot -= 1;
                    }

                    if (IsKeyPressed(ShotTypeSelectKeys[1]))
                    {
                        if (CurrentShot == (ShotType)Enum.GetValues(typeof(ShotType)).Length - 1)
                            CurrentShot = 0;
                        else
                            CurrentShot += 1;
                    }
                }

                else if (AreAnyKeysPressed(ShotTypeHotKeys))
                {
                    foreach (KeyCode key in ShotTypeHotKeys)
                    {
                        if (IsKeyPressed(key))
                        {
                            CurrentShot = (ShotType)Array.IndexOf(ShotTypeHotKeys, key);
                            break;
                        }
                    }
                }
                #endregion
            }

        for (int x = 0; x < Cooldowns.Length; x++)
            if (Cooldowns[x] > 0)
                Cooldowns[x] -= Time.deltaTime;
    }

    /// <param name="keysToCheck">Keys to be checked if pressed</param>
    /// /// <param name="getKey">Use GetKey if true, otherwise use GetKeyDown. false by default</param>
    /// <returns>Returns true if any of the keys are pressed, otherwise returns false</returns>
    bool AreAnyKeysPressed(KeyCode[] keysToCheck, bool getKey = false)
    {
        if (getKey)
            return keysToCheck.Any(k => Input.GetKey(k));
        else
            return keysToCheck.Any(k => Input.GetKeyDown(k));
    }

    /// <param name="keyToCheck">Key to be checked if pressed</param>
    /// /// <param name="getKey">Use GetKey if true, otherwise use GetKeyDown. false by default</param>
    /// <returns>Returns true if the key is pressed, otherwise returns false</returns>
    bool IsKeyPressed(KeyCode keyToCheck, bool getKey = false)
    {
        if (getKey)
            return Input.GetKey(keyToCheck);
        else
            return Input.GetKeyDown(keyToCheck);
    }

    void CannonBall()
    {
        GameObject newBall = CreateProjectile("Cannon Ball", ShotDir.transform.position, CannonBallMat, (int)ShotType.CannonBall);

        newBall.SetActive(true);
        newBall.GetComponent<Rigidbody>().AddForce((ShotDir.transform.position - Barrel.transform.position).normalized * ShotForces[0], ForceMode.Impulse);
    }

    void GrapeShot(int shotAmount)
    {
        float bounds = 0.3f;
        List<GameObject> totalShot = new();
        float radius = ProjectileRadius[(int)ShotType.GrapeShot];

        for (int x = 0; x < shotAmount; x++)
        {
            Vector3 pos = new Vector3(ShotDir.transform.position.x, ShotDir.transform.position.y + GetRandomNumber(bounds + radius), ShotDir.transform.position.z + GetRandomNumber(bounds + radius));
            GameObject newShot = CreateProjectile("Shot", pos, CannonBallMat, (int)ShotType.GrapeShot);
            totalShot.Add(newShot);
        }

        foreach (GameObject shot in totalShot)
        {
            shot.SetActive(true);
            shot.GetComponent<Rigidbody>().AddForce((ShotDir.transform.position - Barrel.transform.position).normalized * (ShotForces[1] * 0.75f), ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Creates a sphere Game Object
    /// </summary>
    /// <param name="name">Name given to projectile</param>
    /// <param name="pos">Starting position of gameobject</param>
    /// <param name="mat">Material applied to gameobject</param>
    /// <param name="shotType">Index for the type of shot to be created</param>
    /// <returns></returns>
    GameObject CreateProjectile(string name, Vector3 pos, Material mat, int shotType)
    {
        GameObject cannonBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        cannonBall.name = name;
        cannonBall.tag = "Shot";
        cannonBall.transform.position = pos;
        cannonBall.transform.localScale = new Vector3(ProjectileRadius[shotType], ProjectileRadius[shotType], ProjectileRadius[shotType]);
        cannonBall.GetComponent<MeshRenderer>().material = mat;

        cannonBall.AddComponent<Rigidbody>();
        Rigidbody rigidBody = cannonBall.GetComponent<Rigidbody>();
        rigidBody.mass = ProjectileMass[shotType];
        rigidBody.drag = ProjectileDrag[shotType];
        rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // Adds damage and knockback to the projectile
        cannonBall.AddComponent<ProjectileController>();
        cannonBall.GetComponent<ProjectileController>().LifeTime = ProjectileLifetime[shotType];
        cannonBall.GetComponent<ProjectileController>().Damage = Damage[shotType] * (1 + (DamageUpgradeLevel[shotType] - 1 / 10));
        cannonBall.GetComponent<ProjectileController>().knockBack = KnockBack[shotType] * (1 + (KnockBackUpgradeLevel[shotType] - 1 / 10));

        return cannonBall;
    }

    float GetRandomNumber(float range)
    {
        return Random.Range(-range, range);
    }

    public void UpgradeDamage(int shotToUpgrade)
    {
        GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gameManager.Resources[ResourceType.gunpowder] >= DamageUpgradeLevel[shotToUpgrade] * UpgradeCostIncrement)
        {
            DamageUpgradeLevel[shotToUpgrade] += 1;
            gameManager.Resources[ResourceType.gunpowder] -= DamageUpgradeLevel[shotToUpgrade] * UpgradeCostIncrement;
        }

    }

    public void UpgradeknockBack(int shotToUpgrade)
    {
        GameManager gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gameManager.Resources[ResourceType.gunpowder] >= KnockBackUpgradeLevel[shotToUpgrade] * UpgradeCostIncrement)
        {
            KnockBackUpgradeLevel[shotToUpgrade] += 1;
            gameManager.Resources[ResourceType.gunpowder] -= KnockBackUpgradeLevel[shotToUpgrade] * UpgradeCostIncrement;
        }  
    }

    public void ActivateDeactivate(bool value)
    {
        Active = value;
    }
}

public enum ShotType
{
    CannonBall,
    GrapeShot
}
