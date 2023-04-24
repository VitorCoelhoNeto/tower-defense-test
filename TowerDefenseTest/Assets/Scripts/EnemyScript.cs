using UnityEngine;

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

    public void TakeDamage(int damageTaken)
    {
        enemyHealth -= damageTaken;

        if(enemyHealth <= 0)
        {
            EnemyDie();
        }
    }

    void EnemyDie()
    {
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

    void EndPath()
    {
        PlayerStatsScript.Lives--;
        Destroy(gameObject);
    }
}
