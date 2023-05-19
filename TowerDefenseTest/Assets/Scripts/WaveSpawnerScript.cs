using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
* This script is in charge of handling the enemy waves spawning
*
* Works in close relationship with the enemy, enemy movement, WaveClass and PlayerStats scripts (EnemyScript.cs, EnemyMovementScript.cs, WaveClassScript.cs and PlayerStatsScript)
*
* Used by GameObjects: GameMaster
*/

// TODO: Only begin new wave on previous one is cleared, or when player wants new wave to spawn
public class WaveSpawnerScript : MonoBehaviour
{   
    // Public variables
    public WaveClassScript[] waves;
    public Transform spawnPoint;
    public Text waveCountdownTimer;
    public float timeBetweenWaves = 5f;
    public static int EnemiesAlive = 0;

    // Private variables
    private float countdown = 2f;
    private int waveNum = 0;

    void Start()
    {
        EnemiesAlive = 0;
    }

    void Update()
    {
        // Do not start a new wave until all enemies from the current wave have been destroyed
        if(EnemiesAlive > 0)
        {
            return;
        }

        // When timer reaches 0 and all enemies from current wave are destroyed, begin next wave and update the countdown on UI
        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownTimer.text = string.Format("{0:00.00}", countdown);//Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        // This is a coroutine because if it wasn't all the enemies would spawn so quickly that they would be on top of each other each wave
        // With the coroutine, we can wait half a second between each enemy spawn
        PlayerStatsScript.roundsSurvived++;

        // For each wave in the list, spawn an enemy with the values from that specific wave in the list (defined in WaveClassScript.cs)
        WaveClassScript wave = waves[waveNum];

        for(int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1 / wave.spawnRate);
        }
        waveNum += 1;

        // When all the waves have been defeated, the level has been won, hence, disable the wave spawner
        if(waveNum == waves.Length)
        {
           Debug.Log("Level won!"); // TODO
           this.enabled = false;
        }
    }

    // Instantiate (spawn) an enemy
    void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++; // This is decremented when an enemy dies or reaches the end of the level (Enemy and Enemy Movement scripts)
    }
}
