using UnityEngine;
using UnityEngine.SceneManagement;

/*
* This script is used to manage what happens in the main menu scene
*
* Works in close relationship with the Scene Fader script (SceneFaderScript.cs)
*
* Used by GameObjects: MainMenu -> MainMenuManager
*/

public class MainMenuScript : MonoBehaviour
{   
    // Public variables
    public string playButtonLoad = "MainLevel_1";
    public SceneFaderScript sceneFader;

    // Button responsible for playing the game
    public void Play()
    {
        sceneFader.FadeTo(playButtonLoad);
    }

    // Button responsible for quitting the game
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}


// List of ideas: TODO
// Video 22 has a lot of comments with cool ideas. 
// Also: Animation on turret node UI arrow up and down
// Also: Health bars only appear after enemies take damage