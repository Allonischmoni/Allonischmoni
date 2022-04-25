using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gyarados : MonoBehaviour
{
    public float fireCountdown;
    public GameObject projectilePrefab;
    public GameObject rainPrefab;
    public float fireRate;
    private Transform target;
    private Enemy targetEnemy;
    public string enemyTag = "Enemy";
    public float turnSpeed;
    public Transform partToRotate;
    public float damage;
    public float range;
    public Animator animator;
    public float secondDamage;
    public float secondFireCountdown;
    public float secondFireRate;
    public float secondRange;

    public float abilityCooldown;
    public GameObject abilityImage;
    private float startAbilityCooldown;
    public Image cooldown;
    public float abilityTime;
    private float startAbilityTime;
    private bool isRaining = false;

    public float damageMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        startAbilityCooldown = abilityCooldown;
        startAbilityTime = abilityTime; 
    }

    // Update is called once per frame
    void Update()
    {
        cooldown.fillAmount = abilityCooldown / startAbilityCooldown;
        if (abilityCooldown >= 0)
        {
            abilityCooldown -= Time.deltaTime;
            abilityImage.SetActive(false);
        }
        else
        {
            abilityImage.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R) && abilityCooldown <= 0)
        {
            StartRaining();
            isRaining = true;
            abilityCooldown = startAbilityCooldown;
        }
        if (isRaining == true)
        {
            abilityTime -= Time.deltaTime;
            DigitalRuby.RainMaker.BaseRainScript rain = rainPrefab.GetComponent<DigitalRuby.RainMaker.BaseRainScript>();
            rain.RainIntensity = 0.1f;
            if (abilityTime <= 0)
            {
                isRaining = false;
                rain.RainIntensity = 0;
                abilityTime = startAbilityTime;
            }
        }
        if (target == null)
        {
            return;
        }
        LockOnTarget();
        if (secondFireCountdown <= 0f)
        {
            CheckingEnemiesInRange();
            secondFireCountdown = 1f / secondFireRate;
        }
        if (fireCountdown <= 0f)
        {
            Attack(target );
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
        secondFireCountdown -= Time.deltaTime;
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
        }
        else
        {
            target = null;
        }
    }
    public void Attack(Transform enemy)
    {
        animator.Play("Base Layer.GaradosNormalAttack", 0, 1f);
        GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, enemy.position, transform.rotation);
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        Destroy(projectile, 2f);
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage, gameObject);
        }
    }
    public void CheckingEnemiesInRange()
    {
        GameObject[] enemies = GameObject .FindGameObjectsWithTag ("Enemy");
        if (enemies.Length >= 5)
        {
            animator.Play("Base Layer.SecondAttack", 0, 1f);
            SecondAttack(enemies );
        }
    }
    public void SecondAttack(GameObject [] enemies)
    {
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) <= secondRange)
            {
                Damage(enemy .transform);
            }
        }
    }
    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(secondDamage, gameObject );
        }
    }
    void StartRaining()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Enemy e = enemy.GetComponent<Enemy>();
            e.dmgMultiplier *= damageMultiplier ;
        }
        animator.Play("Base Layer.WaterFontain", 0, 1f);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
