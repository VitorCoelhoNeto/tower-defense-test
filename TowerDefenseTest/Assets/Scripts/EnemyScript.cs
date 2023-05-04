using UnityEngine;

/*
* This script is used by the enemies to determine what happens when it takes damage and what happens when it dies  
* (used to be used to determine their position, where to move next and what happens when it reaches the end point but this was moved to EnemyMovementScript.cs)
*
* Works in close relationship with the turret, bullet, waypoints and wave spawner scripts (TurretScript.cs, BulletScript.cs, WaypointsScript.cs and WaveSpawnerScript.cs)
*
* Used by GameObjects: Enemy prefabs (ATM Enemy)
*/

public class EnemyScript : MonoBehaviour
{

    // Public Variables
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed; // Speed to be calculated for the slow effect. "HideInInspector" because we want it public to allow move script to modify it but not changeable on inspector
    public float enemyHealth = 100;
    public int enemyValue = 25;
    public GameObject deathEffect;

    // Private variables
    private bool alive;

    void Start()
    {
        speed = startSpeed;
        alive = true;
    }

    // Function to determine what happens when the enemy is damaged
    public void TakeDamage(float damageTaken)
    {
        // damageTaken is a parameter received based on the damage of the bullet invoked on the bullet script
        enemyHealth -= damageTaken;

        // If the health reaches 0, it dies
        if(enemyHealth <= 0)
        {   
            // Check if the enemy is already death or not, to avoid multiple kill triggers due to framerate being too fast and registering several kills at once
            if(alive)
            {
                EnemyDie();
            }
        }
    }

    // Determine what happens to the enemy when it dies, i. e. its health reaches 0
    void EnemyDie()
    {
        alive = false;

        // The player gains money based on the value this enemy has, the death effect is invoked and the enemy is destroyed
        PlayerStatsScript.Money += enemyValue;
        
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        
        Destroy(gameObject);
    }

    // Slow Effect
    public void Slow(float slowAmount)
    {
        speed = startSpeed * (1f - slowAmount);
    }

}
