using UnityEngine;
using UnityEngine.SceneManagement;

/*
* This script is used to manage what happens in the main menu scene
*
* Used by GameObjects: MaineMenu -> MainMenuManager
*/

public class MainMenuScript : MonoBehaviour
{   
    // Public variables
    public string playButtonLoad = "MainLevel_1";

    // Button responsible for playing the game TODO : Scene Transition Fade
    public void Play()
    {
        SceneManager.LoadScene(playButtonLoad);
    }

    // Button responsible for quitting the game
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}


// List of ideas:
// Video 22 has a lot of comments with cool ideas. 
// Also: Animation on turret node UI arrow up and down