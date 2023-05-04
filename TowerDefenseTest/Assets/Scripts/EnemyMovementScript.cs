using UnityEngine;

/*
* This script is used by the enemies to determine their position, where it needs to move next and what happens if they reach the end point
*
* Works in close relationship with the enemy script (EnemyScript.cs)
*
* Used by GameObjects: Enemy prefabs (ATM Enemy)
*/

[RequireComponent(typeof(EnemyScript))]
public class EnemyMovementScript : MonoBehaviour
{
    // Private variables
    private Transform target;
    private int wavepointIndex = 0;
    private EnemyScript enemyScript;

    void Start()
    {
        // Get the enemyScript associated with this same GameObject to get the speed variable
        enemyScript = GetComponent<EnemyScript>();  
        // First waypoint is the first one on the list provided by the Waypoints script
        target = WaypointsScript.waypoints[0];    
    }

    void Update()
    {
        // Move the enemy to the next waypoint by getting the direction from the current position to the waypoint
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemyScript.speed * Time.deltaTime, Space.World);

        // When the enemy reaches the waypoint, get the next one
        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        // Change the speed back to its start speed to cancel out the slowing effects
        enemyScript.speed = enemyScript.startSpeed;
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
