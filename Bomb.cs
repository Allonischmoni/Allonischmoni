using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed;
    public float yMin = 0.5f;
    public float explosionRadius = 20f;
    public int damage = 50;
    public GameObject impactEffect;
    public GameObject shootingTurret;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (transform.position.y <= yMin)
        {
            GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
            Explode();
            Destroy(gameObject);
        }
    }
    public void Seek(GameObject g)
    {
        shootingTurret = g;
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
            e.TakeDamage(damage, shootingTurret);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
