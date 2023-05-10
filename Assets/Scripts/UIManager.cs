using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.WSA;

public class UIManager : MonoBehaviour
{
    // TextMeshPro
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI GunPowerText;
    public TextMeshProUGUI WaveText;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI AmmoCounter;
    public TextMeshProUGUI ShotTypeDisplay;

    // GameObject
    public GameObject BuildUI;
    public GameObject CannonHUD;
    public GameObject[] UpgradeButtons;
    public GameObject[] AquireButtons;

    // Other
    public GameManager GM;
    public CannonController Cannon;

    // Start is called before the first frame update
    void Start()
    {
        
    }    

    void Update()
    {
        GoldText.text = $"Gold: {GM.Resources[ResourceType.gold]}";
        GunPowerText.text = $"GunPowder: {GM.Resources[ResourceType.gunpowder]}";
        WaveText.text = $"Wave: {GM.CurrentWave}";
        HealthText.text = $"Health: {GM.PlayerHealth}";

        if (Cannon.CurrentShot == ShotType.CannonBall)
            AmmoCounter.text = "Ammo: ∞";

        else
            AmmoCounter.text = $"Ammo: {Cannon.ReserveAmmo[(int)Cannon.CurrentShot]}";

        switch (Cannon.CurrentShot)
        {
            case ShotType.CannonBall:
                ShotTypeDisplay.text = "C";
                break;
            case ShotType.GrapeShot:
                ShotTypeDisplay.text = "G";
                break;
            case ShotType.RailGun:
                ShotTypeDisplay.text = "R";
                break;
        }
    }

    public void ActivateBuildUI()
    {
        CannonHUD.SetActive(false);
        BuildUI.SetActive(true);
    }

    public void ActivateCannonHud()
    {
        BuildUI.SetActive(false);
        CannonHUD.SetActive(true);
    }

    public void ShowUpgrades(int shotType)
    {
        AquireButtons[shotType].SetActive(false);
        UpgradeButtons[shotType].SetActive(true);
    }
}
