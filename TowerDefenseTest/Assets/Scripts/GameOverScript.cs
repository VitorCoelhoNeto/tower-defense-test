using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
* This script is used to manage what the game over screen shows, such as no. of rounds survived
*
* Works in close relationship with the game manager and player stats script (GameManagerScript.cs and PlayerStatsScript.cs)
*
* Used by GameObjects: UI -> GameOver
*/

public class GameOverScript : MonoBehaviour
{
    // Scene indexes
    //int MAIN_SCENE = 0;

    // Public variables
    public Text roundsSurvivedText;

    // Unity function like start but it is called on the object's (Game Over screen) enable action instead of the start of the game
    void OnEnable()
    {
        // Update the number of rounds survived
        roundsSurvivedText.text = PlayerStatsScript.roundsSurvived.ToString();
    }

    // Retry button logic, retry game
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Go back to the menu screen
    public void Menu()
    {
        Debug.Log("Go to menu"); // TODO
    }

}
