using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed = 10f;
    public float damageOverTime;

    public float poisonCountdown;
    public float slowingCountdown;

    public bool isSlowed = false;
    public bool isPoisoned;

    public float startHealth = 10;
    private float health;
    public int value = 10;
    public float damageValue;

    public GameObject deathEffect;
    private GameObject poisonTurret;

    public Image healthBar;

    private Transform target;
    private int wavepointIndex = 0;

    private bool isDead = false;

    public float way;

    public bool isNockbacked = false;
    public float nockbackCountdown = 0;

    public bool hasToRotate = false;
    public Transform  partToRotate;
    public float turnSpeed;

    public float dmgMultiplier = 1;

    void Start()
    {
        target = Waypoints.points[0];
        speed  = startSpeed;
        health = startHealth;
    }
    public void SetDamageOverTime(float damageOverTimeAmount)
    {
        damageOverTime = damageOverTimeAmount;
    }
    public void TakeDamage(float amount, GameObject k)
    {
        amount *= dmgMultiplier;
        if (amount >= startHealth)
        {
            amount = startHealth;
        }
        startHealth -= amount;

        healthBar.fillAmount = startHealth /health;


        KillCounter kills = k.GetComponent<KillCounter>();
        kills.KillCounting(amount);

        if (startHealth <= 0 && !isDead)
        {
            Die();
        }
    }
    public void IsPoisoned(GameObject p)
    {
        isPoisoned = true;
        poisonCountdown = 0f;
        poisonTurret = p;
    }
    void Die()
    {
        isDead = true;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
        PlayerStats.Money += value;

        WaveSpawner.enemyAlives--;

        Destroy(gameObject);
    }
    public void Slow(float pct, float time)
    {
        speed = startSpeed * (1f - pct);
        slowingCountdown = time;
        isSlowed = true;
    }
    public void PsychoSlow(float amount)
    {
        speed = startSpeed * (1f - amount);
    }
    public void Nockback(float time)
    {
        speed = startSpeed * -1f;
        isNockbacked = true;
        nockbackCountdown = time;
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (hasToRotate)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        way += speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
        if (isSlowed == true)
        {
            slowingCountdown -= Time.deltaTime;
        }
        if (isPoisoned == true)
        {
            TakeDamage(damageOverTime * Time.deltaTime, poisonTurret);
            poisonCountdown += Time.deltaTime;
        }
        if (isNockbacked == true)
        {
            nockbackCountdown -= Time.deltaTime;
        }
        if (slowingCountdown <= 0f)
        {
            isSlowed = false;
            speed = startSpeed;
            slowingCountdown = 10;
        }
        if (poisonCountdown >= 15f)
        {
            isPoisoned = false;
            poisonCountdown = 0;
        }
        if (nockbackCountdown <= 0f)
        {
            isNockbacked = false;
        }
        if (isSlowed == true || isNockbacked == true)
        {
            return;
        }
        speed = startSpeed;
        
    }
    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.enemyAlives--;
        Destroy(gameObject);
    }
}
