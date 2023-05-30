using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class UIManager : MonoBehaviour
{
    // TextMeshPro
    public TextMeshProUGUI IronText;
    public TextMeshProUGUI TungstenText;
    public TextMeshProUGUI WaveText;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI AmmoCounter;
    public TextMeshProUGUI ShotTypeDisplay;

    // GameObject
    public GameObject BuildUI;
    public GameObject CannonHUD;
    public GameObject[] UpgradeButtons;
    public GameObject[] AquireButtons;
    public GameObject PauseMenu;
    public GameObject InGameUI;

    // Other
    public GameManager GM;
    public CannonController Cannon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        IronText.text = $"Iron: {GM.Resources[ResourceType.iron]}";
        TungstenText.text = $"Tungsten: {GM.Resources[ResourceType.tungsten]}";
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

    public void ShowUpgrades(ShotType shotType)
    {
        AquireButtons[(int)shotType].SetActive(false);
        UpgradeButtons[(int)shotType].SetActive(true);
    }

    public void ShowPauseMenu()
    {
        PauseMenu.SetActive(true);
        InGameUI.SetActive(false);
    }

    public void HidePauseMenu()
    {
        PauseMenu.SetActive(false);
        InGameUI.SetActive(true);
    }

    public void DisplayCost(int cost, ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.iron:
                IronText.text = $"Iron: {GM.Resources[ResourceType.iron]} - {cost}";
                break;
            case ResourceType.tungsten:
                TungstenText.text = $"Tungsten: {GM.Resources[ResourceType.tungsten]} - {cost}";
                break;
        }
    }
}
