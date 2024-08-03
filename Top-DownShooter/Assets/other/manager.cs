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
    private bool isBossWave;
    private bool canStartNextWave = true;
    void Start()
    {
        enemyDamage = 5f;
        enemyHealth = 10f;
        currentWave = 1;
        currentSpawnIndex = 0;
        StartNextWave();
    }

    void Update()
    {
        if (enemiesRemainingToDefeat <= 0 && enemiesRemainingToSpawn <= 0 && canStartNextWave)
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
        if (currentWave % 10 == 0)
        {
            if(!isBossWave)
            {
                isBossWave = true;
                enemiesRemainingToSpawn = 1;
                updateText();
                spawnBoss();
            }
        }
        else
        {
            isBossWave = false;
            enemiesRemainingToSpawn = enemiesPerWave * currentWave;
            enemiesRemainingToDefeat = enemiesRemainingToSpawn;
            Debug.Log("remaining to kill" + enemiesRemainingToDefeat);
            Debug.Log("remaining to spawn" + enemiesRemainingToSpawn);
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
        Debug.Log("remaining to spawn" + enemiesRemainingToSpawn);
        Debug.Log("remaining to kill" + enemiesRemainingToDefeat);
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
        Debug.Log("remaining to kill" + enemiesRemainingToDefeat);
        boss.GetComponent<bossHealth>().OnEnemyDefeated += HandleBossDefeated;
        currentSpawnIndex++;
    }
    void HandleBossDefeated()
    {
        playerhealth.GainHealth(30);
        Playerxp.GainXP(expGain * bossExpMultiplier);
        enemiesRemainingToDefeat--;
        if (enemiesRemainingToDefeat <= 0)
        {
            canStartNextWave = false;
            StartCoroutine(WaitBeforeNextWave(3f));
        }
    }
    IEnumerator WaitBeforeNextWave(float delay)
    {
        yield return new WaitForSeconds(delay);
        canStartNextWave = true;
    }
}
