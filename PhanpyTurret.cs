using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhanpyTurret : MonoBehaviour
{
    private Transform target;
    public Transform hittetEnemy;
    private Enemy targetEnemy;
    private Transform secondTarget;
    private Enemy secondTargetEnemy;
    public Animator animator;
    public Image cooldown;
    public GameObject abbillityImage;
    public GameObject stunEffect;

    [Header("General")]

    public float range = 15f;
    private float damage;
    public float normalDamage;
    public float rollingDamage;
    public bool isRolling = false;
    public float rollingCountdown = 3f;
    public float rollingSpeed = 8f;
    public float lookRange = 10f;
    public float startAbillityCooldown;
    private  float abillityCooldown = 0f;
    public float stun = 1f;
    public float stunTime = 1f;

    [Header("Use Bullets (default)")]

    public float turnSpeed = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public string bigEnemyTag = "BigEnemy";

    public Transform partToRotate;
    public Transform turretPosition;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        animator.SetBool("attack", false);
        cooldown.fillAmount = abillityCooldown / startAbillityCooldown ;
        if (abillityCooldown >= 0)
        {
            abillityCooldown -= Time.deltaTime;
            abbillityImage.SetActive(false);
        }
        else
        {
            abbillityImage.SetActive(true );
        }
        
        if (Input .GetKeyDown(KeyCode.E  ) && abillityCooldown <= 0 && target != null )
        {
            isRolling = true;
            abillityCooldown = startAbillityCooldown;
        }
        if (isRolling == true)
        {
            transform.position += transform.forward * rollingSpeed * Time.deltaTime;
            rollingCountdown -= Time.deltaTime;
            animator.SetBool("isAttacking", true);
            if (rollingCountdown <= 0)
            {
                transform.position = turretPosition .transform .position ;
                isRolling = false;
                animator.SetBool("isAttacking", false);
                rollingCountdown = 1f;
            }
            return;
        }
        if (target == null)
        {
            return;
        }
        LockOnTarget();
        if (fireCountdown <= 0f && Vector3 .Distance(transform .position ,target .transform .position ) <= range )
        {
            
            Attack();
            fireCountdown = 1f / fireRate;
        }
        
        fireCountdown -= Time.deltaTime;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
            Damage(other.gameObject .transform , rollingDamage );
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
        if (nearestEnemy != null && shortestDistance <= lookRange)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void Attack()
    {
        animator.SetBool("attack", true);
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform, normalDamage );
            }
        }
    }
    void Damage(Transform enemy, float d)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(d, gameObject );
            e.Slow(stun, stunTime);
            GameObject effectIns = (GameObject)Instantiate(stunEffect, e.transform.position + new Vector3 (0, 1, 0), e.transform.rotation);
            Destroy(effectIns, 2f);
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.color = Color.green ;
        Gizmos.DrawWireSphere(transform.position, lookRange );
    }
}
