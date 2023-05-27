using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using Random = UnityEngine.Random;
using UnityEngine.Audio;

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
    public float SlowDownFactor;
    // Float | Mouse
    float MouseX;
    float MouseY;
    public float MouseSensitivity;

    // Int
    public int UpgradeCostIncrement;
    int[] DamageUpgradeLevel;
    int[] KnockBackUpgradeLevel;
    public int[] ReserveAmmo;
    public int[] MaxReserveAmmo;
    public int[] RestockPrice;
    public int[] AcquisitionCost;

    // GameObject
    public GameObject Turret;
    public GameObject Barrel;
    public GameObject ShotDir;

    // KeyCode
    public KeyCode[] MovementKeys;
    public KeyCode[] ShotTypeHotKeys;
    public KeyCode[] ShotTypeSelectKeys;
    public KeyCode FireKey;
    public KeyCode SlowDownKey;
    List<KeyCode> Controls;

    // Vector3
    Vector3 BarrelStartRotation;
    Vector3 TurretStartRotation;

    // ShotType
    public ShotType CurrentShot;
    List<ShotType> AquiredShot;

    // Other
    public Material CannonBallMat;
    bool Active;
    AudioSource audioSource;
    PlayerSettings Settings;
    GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        CurrentShot = ShotType.CannonBall;

        // Sets controls and settings from PlayerSettings
        Settings = GameObject.Find("PlayerSettings").gameObject.GetComponent<PlayerSettings>();
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        MovementKeys = Settings.MovementKeys;
        ShotTypeHotKeys = Settings.ShotTypeHotKeys;
        ShotTypeSelectKeys = Settings.ShotTypesSelectKeys;
        FireKey = Settings.FireKey;
        SlowDownKey = Settings.SlowDownKey;
        SlowDownFactor = Settings.SlowDownStrength;
        RotationSpeed = Settings.RotationSpeed;

        Controls = new List<KeyCode>() { FireKey, SlowDownKey };
        Controls.AddRange(MovementKeys);
        Controls.AddRange(ShotTypeHotKeys);
        Controls.AddRange(ShotTypeSelectKeys);

        BarrelStartRotation = Barrel.transform.localEulerAngles;
        TurretStartRotation = Turret.transform.eulerAngles;

        Cooldowns = new float[MaxCooldowns.Length];
        DamageUpgradeLevel = new int[] { 1, 1, 1 };
        KnockBackUpgradeLevel = new int[] { 1, 1, 1 };
        ReserveAmmo = MaxReserveAmmo;
        AquiredShot = new() { ShotType.CannonBall };

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = Settings.SfxVolume;
        MouseSensitivity = Settings.MouseSensitivity;
        if (Input.anyKey && Active)
            if (AreAnyKeysPressed(Controls.ToArray(), true) || AreMouseButtonsPressed())
            {
                #region Shoot cannon with current shot type
                if ((IsKeyPressed(FireKey) && Settings.KeyboardAimingMode) || (Input.GetMouseButtonDown(0) && !Settings.KeyboardAimingMode))
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
                            case ShotType.RailGun:
                                RailGun();
                                break;
                        }

                        audioSource.Play();

                        ReserveAmmo[index] -= 1;
                        if (CurrentShot == ShotType.CannonBall)
                            ReserveAmmo[index] += 1;

                        Cooldowns[index] = MaxCooldowns[index];
                    }
                }
                #endregion

                #region Moving Turret
                if (Settings.KeyboardAimingMode)
                {
                    float rotationamount = RotationSpeed * Time.deltaTime;

                    if (IsKeyPressed(SlowDownKey, true))
                        rotationamount *= SlowDownFactor;

                    // Rotate Turret Left
                    if (IsKeyPressed(MovementKeys[2], true) && !IsKeyPressed(MovementKeys[3], true))
                        RotateLeft(rotationamount);

                    // Rotate Turret Right
                    if (IsKeyPressed(MovementKeys[3], true) && !IsKeyPressed(MovementKeys[2], true))
                        RotateRight(rotationamount);

                    // Angle Barrel Up
                    if (IsKeyPressed(MovementKeys[0], true) && !IsKeyPressed(MovementKeys[1], true))
                        AngleUp(rotationamount);

                    // Angle Barrel Down
                    if (IsKeyPressed(MovementKeys[1], true) && !IsKeyPressed(MovementKeys[0], true))
                        AngleDown(rotationamount);
                }
                #endregion

                #region Change shot Type
                if (AquiredShot.Count > 1)
                {
                    if (AreAnyKeysPressed(ShotTypeSelectKeys))
                    {
                        if (IsKeyPressed(ShotTypeSelectKeys[0]))
                        {
                            int temp = (int)CurrentShot - 1;
                            while (temp != (int)CurrentShot)
                            {
                                if (AquiredShot.Contains((ShotType)temp))
                                {
                                    CurrentShot = (ShotType)temp;
                                    break;
                                }

                                else if (temp < 0)
                                    temp = Enum.GetValues(typeof(ShotType)).Length - 1;
                                else
                                    temp -= 1;
                            }
                        }

                        if (IsKeyPressed(ShotTypeSelectKeys[1]))
                        {
                            int temp = (int)CurrentShot + 1;
                            while (temp != (int)CurrentShot)
                            {
                                if (AquiredShot.Contains((ShotType)temp))
                                {
                                    CurrentShot = (ShotType)temp;
                                    break;
                                }

                                else if (temp == Enum.GetValues(typeof(ShotType)).Length - 1)
                                    temp = 0;
                                else
                                    temp += 1;
                            }
                        }
                    }

                    else if (AreAnyKeysPressed(ShotTypeHotKeys))
                    {
                        foreach (KeyCode key in ShotTypeHotKeys)
                        {
                            if (IsKeyPressed(key))
                            {
                                ShotType shotType = (ShotType)Array.IndexOf(ShotTypeHotKeys, key);
                                if (AquiredShot.Contains(shotType))
                                    CurrentShot = shotType;
                                break;
                            }
                        }
                    }
                }
                #endregion
            }

        // Rotates cannon by mouse movements
        if (!Settings.KeyboardAimingMode && Active && !GM.Paused)
        {
            if (MouseX != Input.GetAxis("Mouse X"))
            {
                float rotateX = MouseX - Input.GetAxis("Mouse X");
                if (rotateX < 0)
                    RotateLeft(rotateX * MouseSensitivity);
                else
                    RotateRight(-rotateX * MouseSensitivity);

                MouseX = Input.GetAxis("Mouse X") + rotateX;
            }

            if (MouseY != Input.GetAxis("Mouse Y"))
            {
                float rotateY = MouseY - Input.GetAxis("Mouse Y");
                if (rotateY < 0)
                    AngleUp(-rotateY * MouseSensitivity);
                else
                    AngleDown(rotateY * MouseSensitivity);

                MouseY = Input.GetAxis("Mouse Y") + rotateY;
            }
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

    /// <summary>
    /// Chekcs if mouse buttons 1 or 2 is pressed
    /// </summary>
    /// <returns>returns true if mouse button 1 or 2 is pressed, otherwise returns false</returns>
    bool AreMouseButtonsPressed()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
    }

    void CannonBall()
    {
        GameObject newBall = CreateProjectile("Cannon Ball", ShotDir.transform.position, (int)ShotType.CannonBall);

        newBall.SetActive(true);
        newBall.GetComponent<Rigidbody>().AddForce((ShotDir.transform.position - Barrel.transform.position).normalized * ShotForces[(int)ShotType.CannonBall], ForceMode.Impulse);
    }

    void GrapeShot(int shotAmount)
    {
        float bounds = 0.3f;
        List<GameObject> totalShot = new();
        float radius = ProjectileRadius[(int)ShotType.GrapeShot];

        for (int x = 0; x < shotAmount; x++)
        {
            Vector3 pos = new Vector3(ShotDir.transform.position.x, ShotDir.transform.position.y + GetRandomNumber(bounds + radius), ShotDir.transform.position.z + GetRandomNumber(bounds + radius));
            GameObject newShot = CreateProjectile("Shot", pos, (int)ShotType.GrapeShot);
            totalShot.Add(newShot);
        }

        foreach (GameObject shot in totalShot)
        {
            shot.SetActive(true);
            shot.GetComponent<Rigidbody>().AddForce((ShotDir.transform.position - Barrel.transform.position).normalized * (ShotForces[(int)ShotType.GrapeShot] * 0.75f), ForceMode.Impulse);
        }
    }

    void RailGun()
    {
        GameObject newBall = CreateProjectile("Rail Gun Sabbot", ShotDir.transform.position, (int)ShotType.RailGun);

        newBall.SetActive(true);
        newBall.GetComponent<Rigidbody>().AddForce((ShotDir.transform.position - Barrel.transform.position).normalized * ShotForces[(int)ShotType.RailGun], ForceMode.Impulse);
    }

    /// <summary>
    /// Creates a sphere Game Object
    /// </summary>
    /// <param name="name">Name given to projectile</param>
    /// <param name="pos">Starting position of gameobject</param>
    /// <param name="mat">Material applied to gameobject</param>
    /// <param name="shotType">Index for the type of shot to be created</param>
    /// <returns></returns>
    GameObject CreateProjectile(string name, Vector3 pos, int shotType)
    {
        GameObject cannonBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        cannonBall.name = name;
        cannonBall.tag = "Shot";
        cannonBall.transform.position = pos;
        cannonBall.transform.localScale = new Vector3(ProjectileRadius[shotType], ProjectileRadius[shotType], ProjectileRadius[shotType]);
        cannonBall.GetComponent<MeshRenderer>().material = CannonBallMat;

        cannonBall.AddComponent<Rigidbody>();
        Rigidbody rigidBody = cannonBall.GetComponent<Rigidbody>();
        rigidBody.mass = ProjectileMass[shotType];
        rigidBody.drag = ProjectileDrag[shotType];
        rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // Adds damage and knockback to the projectile
        cannonBall.AddComponent<ProjectileController>();
        cannonBall.GetComponent<ProjectileController>().LifeTime = ProjectileLifetime[shotType];
        cannonBall.GetComponent<ProjectileController>().Damage = Damage[shotType] * (1 + (DamageUpgradeLevel[shotType] - 1 * 0.5f));
        cannonBall.GetComponent<ProjectileController>().knockBack = KnockBack[shotType] * (1 + (KnockBackUpgradeLevel[shotType] - 1 * 0.5f));

        return cannonBall;
    }

    /// <summary>
    /// Rotate turret left
    /// </summary>
    /// <param name="rotationAmount">The amount to roate</param>
    void RotateLeft(float rotationAmount)
    {
        Vector3 turretRotation = Turret.transform.localEulerAngles;

        if (CurrentRotation + rotationAmount > RotationYMinMax)
        {
            turretRotation = new Vector3(turretRotation.x, TurretStartRotation.y + RotationYMinMax, turretRotation.z);
            CurrentRotation = RotationYMinMax;
        }

        else
        {
            Turret.transform.Rotate(Vector3.back, rotationAmount);
            CurrentRotation += rotationAmount;
        }
    }

    /// <summary>
    /// Angles turret right
    /// </summary>
    /// <param name="rotationAmount">The amount to roate</param>
    void RotateRight(float rotationAmount)
    {
        Vector3 turretRotation = Turret.transform.localEulerAngles;

        if (CurrentRotation - rotationAmount < -RotationYMinMax)
        {
            turretRotation = new Vector3(turretRotation.x, TurretStartRotation.y - RotationYMinMax, turretRotation.z);
            CurrentRotation = -RotationYMinMax;
        }

        else
        {
            Turret.transform.Rotate(Vector3.forward, rotationAmount);
            CurrentRotation -= rotationAmount;
        }

    }

    /// <summary>
    /// Angles cannon barrel up
    /// </summary>
    /// <param name="rotationAmount">The amount to roate</param>
    void AngleUp(float rotationAmount)
    {
        Vector3 barrelRotation = Barrel.transform.localEulerAngles;

        if (CurrentAngle + rotationAmount > RotationUpMax && CurrentAngle! >= RotationUpMax)
        {
            barrelRotation = new Vector3(BarrelStartRotation.x + RotationUpMax, barrelRotation.y, barrelRotation.z);
            CurrentAngle = RotationUpMax;
        }

        else
        {
            Barrel.transform.Rotate(Vector3.right, rotationAmount);
            CurrentAngle += rotationAmount;
        }
    }

    /// <summary>
    /// Angles cannon barrel down
    /// </summary>
    /// <param name="rotationAmount">The amount to roate</param>
    void AngleDown(float rotationAmount)
    {
        Vector3 barrelRotation = Barrel.transform.localEulerAngles;

        if (CurrentAngle - rotationAmount < -RotationDownMin && CurrentAngle! <= -RotationDownMin)
        {
            barrelRotation = new Vector3(BarrelStartRotation.x - RotationDownMin, barrelRotation.y, barrelRotation.z);
            CurrentAngle = -RotationDownMin;
        }

        else
        {
            Barrel.transform.Rotate(Vector3.left, rotationAmount);
            CurrentAngle -= rotationAmount;
        }
    }

    float GetRandomNumber(float range)
    {
        return Random.Range(-range, range);
    }

    public void UpgradeDamage(int shotToUpgrade)
    {
        if (GM.Resources[ResourceType.tungsten] >= DamageUpgradeLevel[shotToUpgrade] * UpgradeCostIncrement)
        {
            GM.Resources[ResourceType.tungsten] -= DamageUpgradeLevel[shotToUpgrade] * UpgradeCostIncrement;
            DamageUpgradeLevel[shotToUpgrade] += 1;
        }

    }

    public void UpgradeknockBack(int shotToUpgrade)
    {
        if (GM.Resources[ResourceType.tungsten] >= KnockBackUpgradeLevel[shotToUpgrade] * UpgradeCostIncrement)
        {
            GM.Resources[ResourceType.tungsten] -= KnockBackUpgradeLevel[shotToUpgrade] * UpgradeCostIncrement;
            KnockBackUpgradeLevel[shotToUpgrade] += 1;
        }
    }

    public void RestockAmmo(int shotToRestock)
    {
        if (GM.Resources[ResourceType.tungsten] >= RestockPrice[shotToRestock])
        {
            GM.Resources[ResourceType.tungsten] -= RestockPrice[shotToRestock];
            ReserveAmmo[shotToRestock] = MaxReserveAmmo[shotToRestock];
        }
    }

    public void AquireShotType(int shotToAquire)
    {
        if (!AquiredShot.Contains((ShotType)shotToAquire) && GM.Resources[ResourceType.tungsten] >= AcquisitionCost[shotToAquire])
        {
            GM.Resources[ResourceType.tungsten] -= AcquisitionCost[shotToAquire];
            AquiredShot.Add((ShotType)shotToAquire);
            GameObject.Find("UI Manager").gameObject.GetComponent<UIManager>().ShowUpgrades((ShotType)shotToAquire);
        }
    }

    public void ActivateDeactivate(bool value)
    {
        Active = value;

        if (Active)
        {
            MouseX = Input.GetAxis("Mouse X");
            MouseY = Input.GetAxis("Mouse Y");
        }
    }

    public int GetCost(ShotType shotToUpgrade, CostType costType)
    {
        int output = 0;

        switch (costType)
        {
            case CostType.Damage:
                output = DamageUpgradeLevel[(int)shotToUpgrade] * UpgradeCostIncrement;
                break;
            case CostType.Knockback:
                output = KnockBackUpgradeLevel[(int)shotToUpgrade] * UpgradeCostIncrement;
                break;
            case CostType.Restock:
                output = RestockPrice[(int)shotToUpgrade];
                break;
            case CostType.Acquisition:
                output = AcquisitionCost[(int)shotToUpgrade];
                break;
        }

        return output;
    }

   
}

[Serializable]
public enum ShotType
{
    CannonBall,
    GrapeShot,
    RailGun
}

public enum CostType
{
    Damage,
    Knockback,
    Restock,
    Acquisition
}
