using UnityEngine;

/*
* This script handles the projectile (bullet) mechanics from the position update, to its destruction and consequent enemy damage
*
* Works in close relationship with the turret and enemy scripts (TurretScript.cs and EnemyScript.cs)
*
* Used by GameObjects: Bullet, Missile
*/

public class BulletScript : MonoBehaviour
{
    // Private variables
    private Transform target;

    // Public variables
    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    public int bulletDamage = 50;

    // Update is called once per frame
    void Update()
    {
        // Destroy the projectile if the target is destroyed (even if the projectile hasn't reached it yet)
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Calculate the direction the projectile must follow to seek the target 
        Vector3 dir = target.position - transform.position;

        // Distance the projectile must move this frame, which equates to the speed of the projectile times the passed time (deltaTime)
        float distanceThisFrame = speed * Time.deltaTime;

        // If the length the projectile is going to travel this frame is smaller than the distance between it and the target, it means the target has been hit
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // If the projectile hasn't hit its target yet, tranlate it in the direction of the target, according to the distance traveled this frame, and rotate it towards it
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    // Logic when the projectile hits the target
    public void HitTarget()
    {
        // Instantiate the effect when the projectile hits its target
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);

        // Check whether the projectile has an AOE (Explosion) or if it is single target (Damage)
        if(explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        // Destroy the projectile on impact
        Destroy(gameObject);
    }

    // If the projectile has an AOE, it means it is an explosion
    void Explode()
    {   
        // When the projectile explodes, a sphere the size of the explosion radius is drawn on the projectile's position to check the affected targets 
        Collider[] collidersList = Physics.OverlapSphere(transform.position, explosionRadius);
        
        // For each affected target, damage the object if it has the "Enemy" tag
        foreach(Collider collider in collidersList)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }

    }

    // Handle the logic related to the damaging of the target enemy
    void Damage(Transform enemy)
    {
        // We can't directly access the general script because each enemy has its own iteration of this script and we want that specific one for each enemy
        EnemyScript e = enemy.GetComponent<EnemyScript>();

        // If the object indeed has an enemy script on it, let that script handle tha damage taken by it in relation to the projectile's damage stat
        if(e != null)
        {
            e.TakeDamage(bulletDamage);
        }
    }

    // Select the target (used by turret script)
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Draw projectile's AOE radius on editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
