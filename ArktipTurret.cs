using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArktipTurret : MonoBehaviour
{
    public float range = 10f;
    public float turnSpeed = 10f;
    public float fireRate = 1f;
    public  float fireCountdown = 0f;
    public float slowPercentage = 0.4f;
    public float slowTime = 10f;

    
    void Update()
    {
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
    public void Shoot()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Slow(collider.transform);
            }
        }
    }
    void Slow(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.Slow(slowPercentage, slowTime );
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
