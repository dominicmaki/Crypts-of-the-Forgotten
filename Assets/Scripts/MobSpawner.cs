using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Reference to the enemy prefab
    public Transform[] spawnPoints;  // Array of spawn points in the level
    public float spawnInterval = 5f;  // Time interval to spawn enemies

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);  // Start spawning enemies periodically
    }

    void SpawnEnemy()
    {
        // Randomly choose a spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate a new enemy at the chosen spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
