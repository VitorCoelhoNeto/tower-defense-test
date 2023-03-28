using UnityEngine;

public class WaypointsScript : MonoBehaviour
{
    public static Transform[] waypoints;

    void Awake()
    {
        // Get all the waypoints available in a list
        waypoints = new Transform[transform.childCount];

        for(int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
