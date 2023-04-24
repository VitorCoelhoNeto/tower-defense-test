using UnityEngine;
using UnityEngine.UI;

/*
* This script updates the UI with the current money the player (check player stats script)
*
* Works in close relationship with the player stats script (PlayerStatsScript.cs)
*
* Used by GameObjects: UI_Scene_Bottom -> Money
*/

public class MoneyUIScript : MonoBehaviour
{
    public Text moneyText;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "§" +  PlayerStatsScript.Money.ToString();
    }
}
