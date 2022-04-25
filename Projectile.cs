using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public float explosionRadius = 10f;
    public int damage = 5;
    public float damageOverTimeP;
    public bool poison;
    private GameObject shootingTurret;

    public int makeMoney = 10;

    public GameObject impactEffect;
    
    public void Seek(Transform _target, GameObject g)
    {
        target = _target;
        shootingTurret = g;
    }
    void Update()
    {
        
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }
    void HitTarget()
    {
        
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 1f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        if (makeMoney > 0)
        {
            PlayerStats.Money += makeMoney;
        }
        
        Destroy(gameObject);
        
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage, shootingTurret );
        }
        if (poison == true)
        {
            e.IsPoisoned(shootingTurret);
            e.SetDamageOverTime(damageOverTimeP);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
