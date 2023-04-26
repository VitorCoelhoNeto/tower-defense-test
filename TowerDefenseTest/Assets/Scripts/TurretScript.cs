using UnityEngine;

/*
* This script is used by the turrets to manage their mechanics such as turning, shooting and finding targets
*
* Works in close relationship with the enemy and bullet scripts (EnemyScript.cs BulletScript.cs)
*
* Used by GameObjects: Turrets prefabs (ATM MissileLauncher, LaserTurret and StandardTurret)
*/

public class TurretScript : MonoBehaviour
{
    // Public variables
    [Header("General")]
    public float range = 15f;

    [Header("Unity Setup Fields")]
    public Transform partToRotate;
    public float rotateSpeed = 10f;
    public string enemyTag = "Enemy"; // What identifies what is an enemy
    public Transform firePoint;

    [Header("Attributes (Uses projectiles, default)")]
    public float fireRate = 1f;
    public GameObject bulletPrefab;

    [Header("Attributes (Uses Laser)")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;

    // Private variables
    private Transform target; // What the turret is supposed to look at (the enemy)
    private float fireCountdown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Call the function "UpdateTarget" every 0,5 seconds (500 ms)
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Gets the closest target
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
        // If there are no enemies nearby, do nothing. If turret uses laser type of projectile, disable the laser
        if(target == null)
        {
            if(useLaser)
            {
                if(lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        // Decide what to do based on the type of projectile used by the turret
        if(useLaser)
        {
            LaserUse();
        }
        else
        {
            // Decides when to shoot, i. e., when the delay between shots reaches 0
            if (fireCountdown <= 0f)
            {
                // If fireCountdown is below or equal to 0, it means the turret is ready to shoot again, resetting its countdown to the inverse of its fire rate in seconds
                // Ex.: If we have a fire rate of 1, it means the turret will shoot every second, if we have a fire rate of 0,25 it means the turret will shoot every 4 seconds
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    // Target lock on
    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position; // Get the direction between the turret and the closest enemy
        Quaternion lookRotation = Quaternion.LookRotation(dir); // Get the rotation of the direction
        //Vector3 rotation = lookRotation.eulerAngles; // this does an instant transition, the below one does a smooth rotation 
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles; // Smoothly rotate between states
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); // Apply the rotation only on the y axis (or else the entire turret would rotate in every direction)
    }

    // What happens if the turret uses the laser projectile type
    void LaserUse()
    {
        // Check if laser is "turned on", if not, turn it on
        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }
        // Set beggining of the laser to the firepoint and the end of the laser to the target's position (hence the 0, and 1, as in beggining of line and end of line)
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
    }

    // Shooting mechanics
    void Shoot()
    {
        // Instantiate the bullet and get its bullet script to assign the target next
        GameObject bulletObj = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletScript bullet = bulletObj.GetComponent<BulletScript>();

        // Invoke the bullet's Seek function to give it the target
        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    // Draw the turret range on editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
