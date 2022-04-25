using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;
    private Transform secondTarget;
    private Enemy secondTargetEnemy;


    [Header("General")]
    
    public float range = 15f;
    public bool hasTwoTargets = false;
    public bool hasThreeTargets = false;
    public bool hasUpgrade = true;
    public bool hasSecondUpgrade = true;

    [Header("Use Bullets (default)")]

    public float turnSpeed = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject projectilePrefab;

    [Header("Use Laser")]

    public bool useLaser = false;
    public bool secondLaser = false;
    public bool thirdLaser = false;
    public LineRenderer lineRenderer;
    public LineRenderer secondLineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public ParticleSystem impactEffect2;
    public Light impactLight2;
    public float damageOverTime = 0;
    public float slowPct = .1f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public string bigEnemyTag = "BigEnemy";

    public Transform partToRotate;

    public Transform firePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        if (hasTwoTargets != false)
        {
            InvokeRepeating("UpdateSecondTarget", 0f, 0.5f);
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        } else
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
            if (distanceToEnemy > longestDistance && distanceToEnemy <= range)
            {
                longestDistance = distanceToEnemy;
                farestEnemy = enemy;
            }
        }
        if (farestEnemy != null && longestDistance <= range)
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
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
                if (secondLaser)
                {
                    if (secondLineRenderer.enabled)
                    {
                        secondLineRenderer.enabled = false;
                        impactEffect2.Stop();
                        impactLight2.enabled = false;
                    }
                }
            }
            return;
        }
        LockOnTarget();

        if (useLaser)
        {
            Laser();
        } else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                if (hasTwoTargets)
                {
                    SecondShoot();
                }
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
        if (secondLaser)
        {
            SecondLaser();
        }

    }
    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime, gameObject );
        targetEnemy.PsychoSlow(slowPct);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

    }
    void SecondLaser()
    {
        secondTargetEnemy.TakeDamage(damageOverTime * Time.deltaTime, gameObject );
        secondTargetEnemy.PsychoSlow(slowPct);

        if (!secondLineRenderer.enabled)
        {
            secondLineRenderer.enabled = true;
            impactEffect2.Play();
            impactLight2.enabled = true;
        }
        secondLineRenderer.SetPosition(0, firePoint.position);
        secondLineRenderer.SetPosition(1, secondTarget.position);

        Vector3 dir = firePoint.position - secondTarget.position;

        impactEffect2.transform.position = secondTarget.position + dir.normalized;

        impactEffect2.transform.rotation = Quaternion.LookRotation(dir);
    }
    void Shoot()
    {
        GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Seek(target, gameObject);

        }
    }
    void SecondShoot()
    {
        GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.Seek(secondTarget, gameObject);

        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
