using UnityEngine;
using UnityEngine.SceneManagement;

/*
* This script is used to manage what happens when the game complete screen shows
*
* Works in close relationship with the game manager, scene fader and player stats scripts (GameManagerScript.cs, SceneFaderScript.cs and PlayerStatsScript.cs)
*
* Used by GameObjects: UI -> LevelComplete
*/

public class LevelCompleteScript : MonoBehaviour
{
    // Public variables
    public string menuScene = "MainMenu";
    public SceneFaderScript sceneFader;
    public string nextLevel = "MainLevel_2";
    public int levelToUnlock = 2;

    // Continue button logic. Continue to next level
    public void Continue()
    {
        // TODO FIX THIS!!!, when we complete level 1, level unlocked is 2, if we complete level 2, its 3, but if we complete 1 again it goes back to 2
        // This should also be done in the "WinLevel", not here, because if we press "Menu", the next level isn't actually unlocked TODO
        PlayerPrefs.SetInt("levelReached", levelToUnlock); 
        sceneFader.FadeTo(nextLevel);
    }

    // Go back to the menu screen
    public void Menu()
    {
        sceneFader.FadeTo(menuScene);
    }
}
