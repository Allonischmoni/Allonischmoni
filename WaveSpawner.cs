using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveSpawner : MonoBehaviour
{
    public static int enemyAlives = 0;

    public Wave[] waves;

    public Transform enemyPrefab;

    public Transform spawnLocation;

    public float timeBetweenWaves = 5.5f;
    private float countdown = 2f;

    public Text waveCountdownText;
    public Text waveText;

    private int waveNumber = 0;

    

    public Text moneyManagementText;

    void Start()
    {
        countdown = 5f;
        enemyAlives = 0;
    }

    void Update()
    {
        if (enemyAlives > 0)
        {
            return;
        }
        if (waveNumber == waves.Length)
        {
            Debug.Log("Level Won!");
            this.enabled = false;
        }
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
        waveText.text = "Wave " + waveNumber; 
    }
    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveNumber];
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveNumber++;
        
        if (waveNumber == waves.Length)
        {
            Debug.Log("LEVEL WON!");
            this.enabled = false;
        }
    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnLocation.position, spawnLocation.rotation);
        enemyAlives++;
    }
}
