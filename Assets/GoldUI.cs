using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldUI : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    void Update()
    {
        goldText.text = "Gold: " + PlayerStats.Gold.ToString();
    }
}
