using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    // Public Variables
    public float speed = 10f;

    // Private variables
    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        // First waypoint is the first one on the list provided by the Waypoints script
        target = WaypointsScript.waypoints[0];    
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
            Destroy(gameObject);
        }
        else
        {
            // If there are more waypoints remaining, get the next one
            wavepointIndex++;
            target = WaypointsScript.waypoints[wavepointIndex];
        }
        
    }
}
