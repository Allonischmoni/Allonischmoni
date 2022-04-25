using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlurakTurret : MonoBehaviour
{
    public Transform centrePoint;
    public float speed;
    public float fireCountdown;
    public GameObject projectilePrefab;
    public float fireRate;
    public Transform firePoint;
    private Transform target;
    private Enemy targetEnemy;
    public string enemyTag = "Enemy";
    public Transform End;
    private Transform secondTarget;
    public GameObject secondProjectilePrefab;
    private Enemy secondTargetEnemy;
    public float secondFireCountdown;
    public float secondfireRate;

    

    void Start()
    {
        fireCountdown = 5f;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        InvokeRepeating("UpdateSecondTarget", 0f, 0.5f);
        transform.position += new Vector3(-4f, 3f, 0f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform .position );
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
    void UpdateSecondTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float longestDistance = 0f;
        GameObject farestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy > longestDistance)
            {
                longestDistance  = distanceToEnemy;
                farestEnemy = enemy;
            }
        }
        if (farestEnemy != null)
        {
            secondTarget = farestEnemy.transform;
            secondTargetEnemy = farestEnemy.GetComponent<Enemy>();
        }
        else
        {
            secondTarget = null;
        }

    }
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.Rotate(0f, 30f * Time.deltaTime, 0f);
        if (target == null)
        {
            return;
        }
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        if (secondFireCountdown <= 0f)
        {
            SecondShoot();
            secondFireCountdown = 1f / secondfireRate;
        }
        secondFireCountdown -= Time.deltaTime;
        fireCountdown -= Time.deltaTime;
    }
    void Shoot()
    {
        GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Seek(target, gameObject );

        }
    }
    void SecondShoot()
    {
        GameObject projectileGO = (GameObject)Instantiate(secondProjectilePrefab , firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Seek(secondTarget, gameObject );

        }
    }
}
