using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
* This script is in charge of handling the number of rounds survived in the game over / game victory screens
*
* Used by GameObjects: UI -> GameOver -> LevelComplete/GameOver -> RoundsSurvived -> NoRoundsText
*/

public class RoundsSurvivedScript : MonoBehaviour
{
    // Public variables
    public Text roundsSurvivedText;

    // Unity function like start but it is called on the object's (Game Over screen) enable action instead of the start of the game
    void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    // Animate the survived rounds from 0 to the number of survived rounds
    IEnumerator AnimateText()
    {
        roundsSurvivedText.text = "0";
        int round = 0;

        // Wait for 0,7 seconds to allow for the animation to play out before starting the round counting up animation
        yield return new WaitForSeconds(.7f);

        while(round < PlayerStatsScript.roundsSurvived)
        {
            round++;
            roundsSurvivedText.text = round.ToString();
            yield return new WaitForSeconds(.05f);
        }
    }
}
