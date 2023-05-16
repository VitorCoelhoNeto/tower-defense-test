using UnityEngine;

/*
* This script represents the "Wave" object
*
* Works in close relationship with the Wave Spanwer script (WaveSpanwerScript.cs)
*
* Used by GameObjects: None - This is a class
*/

[System.Serializable]
public class WaveClassScript 
{
    // Public variables
    public GameObject enemy; // Which enemy that wave is going to spawn TODO : Add a list to allow for waves to spawn different kinds of enemies
    public int enemyCount; // How many enemies that wave is going to spawn TODO : Randomize the enemies based on weights attributed to each enemy from list above
    public float spawnRate; // The spawn rate of the enemies

}
