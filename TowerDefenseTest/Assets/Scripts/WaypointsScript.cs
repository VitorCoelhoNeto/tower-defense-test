using UnityEngine;

/*
* This script handles the waypoints that are gotten from the game level
*
* Works in close relationship with the enemy script (EnemyScript.cs)
*
* Used by GameObjects: Waypoints
*/

public class WaypointsScript : MonoBehaviour
{
    public static Transform[] waypoints;

    void Awake()
    {
        // Get all the waypoints available into a list, from the Waypoints gameobject children list
        waypoints = new Transform[transform.childCount];

        for(int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
