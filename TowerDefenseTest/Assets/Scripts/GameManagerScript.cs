using UnityEngine;

/*
* This script checks whether the game is over or not according to curret lives left
*
* Works in close relationship with the player stats script (PlayerStatsScript.cs)
*
* Used by GameObjects: GameMaster
*/

public class GameManagerScript : MonoBehaviour
{
    private bool gameOver = false;

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
        Debug.Log("Game Over!"); //TODO
    }
}
