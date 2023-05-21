using UnityEngine;
using UnityEngine.UI;

/*
* This script checks whether the game is over or not according to curret lives left
*
* Works in close relationship with the player stats, wave spawner, Pause Menu and the GameOver Script 
* (PlayerStatsScript.cs, WaveSpawnerScript.cs, PauseMenuScript.cs and GameOverScript.cs)
*
* Used by GameObjects: GameMaster
*/

public class GameManagerScript : MonoBehaviour
{
    // Public variables
    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    public Button speedUpButton;
    public Sprite slowImage;
    public Sprite fastImage;
    public static bool gameOver; // We have to set this to true on start because static variables carry their values on scene change
    public static bool isSpedUp = false;

    void Start()
    {
        gameOver = false;
    }
    
    // Update is called once per frame
    void Update()
    {   
        // Stop "ending" the game when the game is already over
        if(gameOver)
        {
            return;
        }
        if(PlayerStatsScript.Lives <= 0)
        {
            EndGame();
        }
    }

    // If there aren't any more lives left, end the game
    private void EndGame()
    {
        gameOver = true;
        if(isSpedUp)
        {
            ToggleSpeedUp();
        }
        speedUpButton.enabled = false;
        gameOverUI.SetActive(true);
    }

    // What happens when the level has been won (shows game won screen and increments 1 to the "levelReached" player pref)
    public void WinLevel()
    {
        gameOver = true;
        completeLevelUI.SetActive(true);
    }

    // Game Speed Controll
    public void ToggleSpeedUp()
    {
        if(!isSpedUp)
        {
            speedUpButton.image.sprite = fastImage;
            Time.timeScale = 2f;
            isSpedUp = true;
        }
        else
        {
            speedUpButton.image.sprite = slowImage;
            Time.timeScale = 1f;
            isSpedUp = false;
        }
    }
}
