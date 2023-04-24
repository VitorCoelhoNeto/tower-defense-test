using UnityEngine;

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
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;

        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    public void HitTarget()
    {
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);

        if(explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] collidersList = Physics.OverlapSphere(transform.position, explosionRadius);
        
        foreach(Collider collider in collidersList)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }

    }

    void Damage(Transform enemy)
    {
        // We can't directly access the general script because each enemy has its own iteration of this script and we want that specific one for each enemy
        EnemyScript e = enemy.GetComponent<EnemyScript>();

        if(e != null)
        {
            e.TakeDamage(bulletDamage);
        }
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
