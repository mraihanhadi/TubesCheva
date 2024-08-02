using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class manager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab; 
    public Transform[] spawnPoints; 
    public int enemiesPerWave = 3; 
    public float timeBetweenWaves = 2f;
    public playerxp Playerxp;
    public PlayerHealth playerhealth;
    public TextMeshProUGUI WaveText;
    public float enemyDamage = 5f;
    public float enemyHealth = 10f;
    public float expGain = 10f;
    public float bossDamage = 25f;
    public float bossHealth = 300f;
    public float bossExpMultiplier = 3f;

    private int enemiesRemainingToSpawn;
    private int enemiesRemainingToDefeat;
    private int currentWave;
    private int currentSpawnIndex;
    void Start()
    {
        enemyDamage = 5f;
        enemyHealth = 10f;
        currentWave = 8;
        currentSpawnIndex = 0;
        StartNextWave();
    }

    void Update()
    {
        if (enemiesRemainingToDefeat <= 0 && enemiesRemainingToSpawn <= 0)
        {
            currentWave++;
            if (currentWave % 5 == 0)
            {
                increaseEnemyStats();
            }
            StartNextWave();
        }
    }

    void StartNextWave()
    {
        if (currentWave % 10 == 0 && enemiesRemainingToDefeat == 0 && enemiesRemainingToSpawn == 0)
        {
            enemiesRemainingToSpawn = 1;
            spawnBoss();
        }
        else
        {
            enemiesRemainingToSpawn = enemiesPerWave * currentWave;
            enemiesRemainingToDefeat = enemiesRemainingToSpawn;
            updateText();
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesRemainingToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.15f);
        }
        enemiesRemainingToSpawn = 0;
    }

    void SpawnEnemy()
    {
        if (currentSpawnIndex >= spawnPoints.Length)
        {
            currentSpawnIndex = 0;
        }

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPoints[currentSpawnIndex].position, Quaternion.identity);
        enemy.GetComponent<EnemyHealth>().OnEnemyDefeated += HandleEnemyDefeated;

        currentSpawnIndex++;
    }

    void HandleEnemyDefeated()
    {
        playerhealth.GainHealth(5);
        Playerxp.GainXP(expGain);
        enemiesRemainingToDefeat--;
    }
    void updateText()
    {
        WaveText.text = $"Wave {currentWave}";
    }
    void increaseEnemyStats()
    {
        enemyDamage *= 1.25f;
        enemyHealth *= 1.5f;
        expGain *= 1.05f;
    }
    void spawnBoss()
    {
        if (currentSpawnIndex >= spawnPoints.Length)
        {
            currentSpawnIndex = 0;
        }
        GameObject boss = Instantiate(bossPrefab, spawnPoints[currentSpawnIndex].position, Quaternion.identity);
        enemiesRemainingToDefeat = 1;
        enemiesRemainingToSpawn =  0;
        boss.GetComponent<bossHealth>().OnEnemyDefeated += HandleBossDefeated;
        currentSpawnIndex++;
    }
    void HandleBossDefeated()
    {
        playerhealth.GainHealth(20);
        Playerxp.GainXP(expGain * bossExpMultiplier);
        enemiesRemainingToDefeat--;
    }
}
