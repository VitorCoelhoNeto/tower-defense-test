using UnityEngine;
using UnityEngine.SceneManagement;

/*
* This script is used to manage what the game over screen shows
*
* Works in close relationship with the game manager, scene fader and player stats scripts (GameManagerScript.cs, SceneFaderScript.cs and PlayerStatsScript.cs)
*
* Used by GameObjects: UI -> GameOver
*/

public class GameOverScript : MonoBehaviour
{
    // Scene indexes
    //int MAIN_SCENE = 0;

    // Public variables
    public string menuScene = "MainMenu";
    public SceneFaderScript sceneFader;

    // Retry button logic, retry game
    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    // Go back to the menu screen
    public void Menu()
    {
        sceneFader.FadeTo(menuScene);
    }

}
