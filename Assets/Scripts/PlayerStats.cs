using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int startMoney = 200;

    void Start()
    {
        Money = startMoney;
    }
}
