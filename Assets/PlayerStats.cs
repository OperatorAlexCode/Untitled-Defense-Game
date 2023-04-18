using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Gold;
    public int startGold = 500;

    void Start()
    {
        Gold = startGold;
    }
}
