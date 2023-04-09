using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    // Public variables
    public static int Money;
    public int startMoney = 400;

    void Start()
    {
        Money = startMoney;
    }
}
