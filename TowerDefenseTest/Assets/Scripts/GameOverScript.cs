using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
* This script is used to manage what the game over screen shows, such as no. of rounds survived
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
    public Text roundsSurvivedText;
    public string menuScene = "MainMenu";
    public SceneFaderScript sceneFader;

    // Unity function like start but it is called on the object's (Game Over screen) enable action instead of the start of the game
    void OnEnable()
    {
        // Update the number of rounds survived
        roundsSurvivedText.text = PlayerStatsScript.roundsSurvived.ToString();
    }

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
