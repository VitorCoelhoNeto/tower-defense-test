using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        for(int i = 0; i < waveNum; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
