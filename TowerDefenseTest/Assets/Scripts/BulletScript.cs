using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Private variables
    private Transform target;

    // Public variables
    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;

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
        Destroy(enemy.gameObject);
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
