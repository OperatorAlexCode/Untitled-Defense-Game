using System.Collections;
using System.Collections.Generic;
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
    
    // GameObject
    public GameObject BuildUI;
    public GameObject CannonHUD;
    public GameObject[] UpgradeButtons;
    public GameObject[] AquireButtons;

    // Other
    public GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        
    }    

    void Update()
    {
        GoldText.text = $"Gold: {GM.Resources[ResourceType.gold]}";
        GunPowerText.text = $"GunPower: {GM.Resources[ResourceType.gunpowder]}";
        WaveText.text = $"Wave: {GM.CurrentWave}";
        HealthText.text = $"Health: {GM.PlayerHealth}";
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
