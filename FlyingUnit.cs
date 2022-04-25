using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingUnit : MonoBehaviour
{
    private Transform target;
    public float speed;
    private Enemy targetEnemy;
    public string enemyTag = "Enemy";
    private Enemy enemyInformation;
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public float attackCountdown;
    public float attackRate;
    public float damage;
    public Animator animator;
    public GameObject impactEffect;
    public float nockback;
    public float radius;

    void Start()
    {
        attackCountdown = 2f;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        animator.SetBool("attack", false);
        if (target == null)
        {
            return;
        }
        if (Vector3.Distance(transform.position, target.position) >= 1f)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }   
        LockOnTarget();
        if (Vector3.Distance(transform.position, target.position) <= 4f)
        {
            if (attackCountdown <= 0f)
            {
                Attack(target);
                
                attackCountdown = 1f / attackRate;
            }
        }
        attackCountdown -= Time.deltaTime;
    }
    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float longestDistance = 0;
        GameObject furthestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            enemyInformation = enemy.GetComponent<Enemy>();
            float coveredWay = enemyInformation.way;
            if (coveredWay  > longestDistance)
            {
                longestDistance = coveredWay;
                furthestEnemy = enemy;
            }
        }
        if (furthestEnemy != null)
        {
            target = furthestEnemy.transform;
            targetEnemy = furthestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
    void Attack(Transform Enemy)
    {
        if (radius > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    Damage(collider.transform);
                    Enemy c = collider.GetComponent<Enemy>();
                    c.Nockback(nockback);
                }
            }
        } else
        {
            Damage(target);
        }
        
        animator.SetBool("attack", true);
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
    }
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage, gameObject );
        }
    }
}
