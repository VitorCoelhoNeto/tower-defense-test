using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
* This script is in charge of handling the enemy waves spawning
*
* Works in close relationship with the enemy and PlayerStats scripts (EnemyScript.cs and PlayerStatsScript)
*
* Used by GameObjects: GameMaster
*/

// TODO: Only begin new wave on previous one is cleared, or when player wants new wave to spawn
public class WaveSpawnerScript : MonoBehaviour
{   
    // Public variables
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Text waveCountdownTimer;
    public float timeBetweenWaves = 5f;

    // Private variables
    private float countdown = 2f;
    private int waveNum = 0;

    void Update()
    {
        // When timer reaches 0, begin next wave and update the countdown on UI
        if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownTimer.text = string.Format("{0:00.00}", countdown);//Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        // This is a coroutine because if it wasn't all the enemies would spawn so quickly that they would be on top of each other each wave
        // With the coroutine, we can wait half a second between each enemy spawn

        // Spawn as many enemies as the wave number
        waveNum += 1;
        PlayerStatsScript.roundsSurvived++;
        for(int i = 0; i < waveNum; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Instantiate (spawn) an enemy
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
