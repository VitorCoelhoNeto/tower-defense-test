using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    // Public variables
    public Transform partToRotate;
    public float range = 15f;
    public float rotateSpeed = 10f;
    public string enemyTag = "Enemy"; // What identifies what is an enemy

    // Private variables
    private Transform target; // What the turret is supposed to look at (the enemy)

    // Start is called before the first frame update
    void Start()
    {
        // Call the function "UpdateTarget" every 0,5 seconds (500 ms)
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        // Get all enemies (GameObjects with "Enemy" as Tag) every 0,5 seconds
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;

        // Get the nearest enemy to the turret
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // If there are enemies in range, choose the closest one as the target
        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If there are no enemies nearby, do nothing
        if(target == null)
        {
            return;
        }

        // Target lock on
        Vector3 dir = target.position - transform.position; // Get the direction between the turret and the closest enemy
        Quaternion lookRotation = Quaternion.LookRotation(dir); // Get the rotation of the direction
        //Vector3 rotation = lookRotation.eulerAngles; // this does an instant transition, the below one does a smooth rotation 
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles; // Smoothly rotate between states
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); // Apply the rotation only on the y axis (or else the entire turret would rotate in every direction)
    }

    void OnDrawGizmosSelected()
    {
        // Draw the turret range on editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
