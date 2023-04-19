using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI GunPowerText;
    public TextMeshProUGUI WaveText;
    public TextMeshProUGUI HealthText;
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
}
