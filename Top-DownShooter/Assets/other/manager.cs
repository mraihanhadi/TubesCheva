using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public Transform[] spawnPoints; 
    public int enemiesPerWave = 3; 
    public float timeBetweenWaves = 2f;
    public playerxp Playerxp;

    private int enemiesRemainingToSpawn;
    private int enemiesRemainingToDefeat;
    private int currentWave;
    private int currentSpawnIndex;
    void Start()
    {
        currentWave = 1;
        currentSpawnIndex = 0;
        StartNextWave();
    }

    void Update()
    {
        if (enemiesRemainingToDefeat <= 0 && enemiesRemainingToSpawn <= 0)
        {
            currentWave++;
            StartNextWave();
        }
    }

    void StartNextWave()
    {
        enemiesRemainingToSpawn = enemiesPerWave * currentWave;
        enemiesRemainingToDefeat = enemiesRemainingToSpawn;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesRemainingToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
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
        Playerxp.GainXP(10);
        enemiesRemainingToDefeat--;
    }

}
