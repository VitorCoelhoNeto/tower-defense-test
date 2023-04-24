using UnityEngine;

/*
* This script is used by the enemies to determine its position, where it needs to move next, what happens when it takes damage, what happens when it dies and what happens
* when it reaches the end point
*
* Works in close relationship with the turret, bullet, waypoints and wave spawner scripts (TurretScript.cs, BulletScript.cs, WaypointsScript.cs and WaveSpawnerScript.cs)
*
* Used by GameObjects: Enemy prefabs (ATM Enemy)
*/

public class EnemyScript : MonoBehaviour
{

    // Public Variables
    public float speed = 10f;
    public int enemyHealth = 100;
    public int enemyValue = 25;
    public GameObject deathEffect;

    // Private variables
    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        // First waypoint is the first one on the list provided by the Waypoints script
        target = WaypointsScript.waypoints[0];    
    }

    // Function to determine what happens when the enemy is damaged
    public void TakeDamage(int damageTaken)
    {
        // damageTaken is a parameter received based on the damage of the bullet invoked on the bullet script
        enemyHealth -= damageTaken;

        // If the health reaches 0, it dies
        if(enemyHealth <= 0)
        {
            EnemyDie();
        }
    }

    // Determine what happens to the enemy when it dies, i. e. its health reaches 0
    void EnemyDie()
    {
        // The player gains money based on the value this enemy has, the death effect is invoked and the enemy is destroyed
        PlayerStatsScript.Money += enemyValue;
        
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        
        Destroy(gameObject);
    }

    void Update()
    {
        // Move the enemy to the next waypoint by getting the direction from the current position to the waypoint
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // When the enemy reaches the waypoint, get the next one
        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        // If there are no more waypoints, the enemy has reached the end, so destroy it (and the player loses a life)
        if(wavepointIndex >= WaypointsScript.waypoints.Length - 1)
        {   
            EndPath();
            return;
        }
        else
        {
            // If there are more waypoints remaining, get the next one
            wavepointIndex++;
            target = WaypointsScript.waypoints[wavepointIndex];
        }
    }

    // This function determines what happens if the enemy reaches the end of the map
    void EndPath()
    {
        // The player loses a life and the enemy is destroyed
        PlayerStatsScript.Lives--;
        Destroy(gameObject);
    }
}
