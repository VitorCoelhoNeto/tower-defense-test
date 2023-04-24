using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        if(gameOver)
        {
            return;
        }
        if(PlayerStatsScript.Lives <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameOver = true;
        Debug.Log("Game Over!"); //TODO
    }
}
