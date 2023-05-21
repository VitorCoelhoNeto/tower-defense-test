using UnityEngine;

/*
* This script checks whether the game is over or not according to curret lives left
*
* Works in close relationship with the player stats, wave spawner and the GameOver Script (PlayerStatsScript.cs, WaveSpawnerScript.cs and GameOverScript.cs)
*
* Used by GameObjects: GameMaster
*/

public class GameManagerScript : MonoBehaviour
{
    // Public variables
    public GameObject gameOverUI;
    public static bool gameOver; // We have to set this to true on start because static variables carry their values on scene change

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
        gameOverUI.SetActive(true);
    }

    // What happens when the level has been won (shows game won screen and increments 1 to the "levelReached" player pref)
    public void WinLevel()
    {
        Debug.Log("Level won!"); // TODO
        PlayerPrefs.SetInt("levelReached", PlayerPrefs.GetInt("levelReached", 1) + 1);
    }
}
