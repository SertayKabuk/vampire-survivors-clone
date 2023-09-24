using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public class Wave
    {
        public string waveName;
        public int waveQuota; // total number of enemies in this wave
        public float spawnInterval; // frequency of spawn
        public int spawnCount;  // already spawned enemy count
        public List<EnemyGroup> enemyGroups;
    }
    [Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; // number of enemies to spawn
        public int spawnCount; // already spawn enemy count
        public GameObject enemyPrefab;
    }
    public List<Wave> waves; //all waves
    public int currentWaveCount = 0;

    [Header("Spawner Attributes")]
    float spawnTimer; // spawn enemies in wave
    public float waveInterval; // timer for next wave in seconds
    public float aliveEnemyCount;
    public float maxAllowedEnemyCount;
    public bool haltEnemySpawn = false;

    [Header("SpawnPoints")]
    public List<Transform> relativeEnemySpawnPoints;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaveCount < waves.Count - 1 && waves[currentWaveCount].spawnCount == 0)//if current wave ended begin next wave
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        //spawn enemies continuously
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);//waits

        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;

        foreach (EnemyGroup enemyGroup in waves[currentWaveCount].enemyGroups) // calculate total enemy count to spawn
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }

    void SpawnEnemies()
    {
        if (aliveEnemyCount < maxAllowedEnemyCount) haltEnemySpawn = false;

        if (haltEnemySpawn) return;

        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota) // if there is stilll enemies to spawn
        {
            foreach (EnemyGroup enemyGroup in waves[currentWaveCount].enemyGroups)//spawn each enemy to enemyCount
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    var relativeEnemyPosition = relativeEnemySpawnPoints[UnityEngine.Random.Range(0, relativeEnemySpawnPoints.Count - 1)].position; //get random position

                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeEnemyPosition, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    aliveEnemyCount++;

                    if (aliveEnemyCount >= maxAllowedEnemyCount) haltEnemySpawn = true;
                }
            }
        }
    }

    public void OnEnemyKilled()
    {
        aliveEnemyCount--;
    }
}