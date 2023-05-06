using UnityEngine;

/*
* This script stores all the information about the current game being played and its player's stats
*
* Works in close relationship with the BuildManager, Enemy, GameManager, Lives, Money and WaveSpawner scripts (BuildManagerScript.cs, EnemyScript.cs, GameManagerScript.cs, 
* LivesScript.cs, MoneyScript.cs and WaveSpawnerScript.cs)
*
* Used by GameObjects: GameMaster
*/

public class PlayerStatsScript : MonoBehaviour
{
    // Public variables
    public static int Money;
    public int startMoney = 400;
    public static int Lives;
    public int startLives = 20;
    public static int roundsSurvived;

    // Initiate the static variables with their start amount
    void Start()
    {
        Money = startMoney;
        Lives = startLives;
        roundsSurvived = 0;
    }
}
