using UnityEngine;
using UnityEngine.SceneManagement;

/*
* This script implements the pause game feature (toggles the pause menu and freezes time)
*
* Works in close relationship with the Scene Fader and Game Manager scripts (SceneFaderScript.cs and GameManagerScript.cs)
*
* Used by GameObjects: GameMaster
*/

public class PauseMenuScript : MonoBehaviour
{
    // Public variables
    public GameObject pauseUI;
    public SceneFaderScript sceneFader;
    public GameManagerScript gameManagerScript;
    public string menuScene = "MainMenu";

    void Update()
    {
        // If either the "ESC" or the "P" keys are pressed, the game pauses
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    // Function that toggles the pause menu, based on its current state (inverts it) and freezes time
    public void TogglePause()
    {
        if(GameManagerScript.isSpedUp)
        {
            gameManagerScript.ToggleSpeedUp();
        }

        pauseUI.SetActive(!pauseUI.activeSelf);

        // If the pause menu is open, freeze time, else unfreeze it
        if(pauseUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    // Retry button reloads scene
    public void Retry()
    {   
        TogglePause();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    // Menu button goes back to the main menu
    public void Menu()
    {
        TogglePause();
        sceneFader.FadeTo(menuScene);
    }
}
