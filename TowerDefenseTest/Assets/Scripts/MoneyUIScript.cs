using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUIScript : MonoBehaviour
{
    public Text moneyText;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "§" +  PlayerStatsScript.Money.ToString();
    }
}
