using UnityEngine;
using UnityEngine.UI;

/*
* This script updates the text on the UI with the current lives the player has
*
* Works in close relationship with the enemy script and the player stats script (EnemyScript.cs and PlayerStatsScript.cs)
*
* Used by GameObjects: UI_Scene_Top -> Lives (Text)
*/

public class LivesUIScript : MonoBehaviour
{
    public Text livesText;

    // Update is called once per frame
    void Update()
    {
            livesText.text = PlayerStatsScript.Lives.ToString() + " LIVES";
    }
}
