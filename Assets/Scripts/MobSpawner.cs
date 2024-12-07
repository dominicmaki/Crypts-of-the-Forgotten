using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public MobSO[] mobVariants;      // Array of mob types (ScriptableObjects)
    public Transform[] spawnPoints;  // Spawn points for enemies
    [SerializeField] public float spawnRadius = 10f;  // Detection radius around each spawn point
    [SerializeField] public float spawnCooldown = 3f; // Time interval to prevent continuous spawning at a spawn point
    private float[] lastSpawnTime;   // Track last spawn time for each spawn point

    public Transform player;         // Reference to the player's transform

    void Start()
    {
        if (spawnPoints.Length > 0 && mobVariants.Length > 0)
        {
            lastSpawnTime = new float[spawnPoints.Length]; // Initialize spawn timers for each spawn point
        }
        else
        {
            Debug.LogError("Ensure spawn points and mob variants are assigned.");
        }
    }

    void Update()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            float distance = Vector3.Distance(player.position, spawnPoints[i].position);

            // Check if spawn point is within radius and cooldown has passed
            if (distance <= spawnRadius && Time.time - lastSpawnTime[i] >= spawnCooldown)
            {
                // Spawn an enemy at this spawn point
                SpawnEnemy(i);

                // Reset the last spawn time
                lastSpawnTime[i] = Time.time;
            }
        }
    }

    void SpawnEnemy(int spawnPointIndex)
    {
        if (mobVariants.Length == 0)
        {
            Debug.LogWarning("No mob variants available.");
            return;
        }

        // Randomly select a mob variant
        MobSO chosenMob = mobVariants[Random.Range(0, mobVariants.Length)];

        // Spawn the mob at the nearest spawn point
        GameObject spawnedMob = Instantiate(chosenMob.mobPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        Mob mobScript = spawnedMob.GetComponent<Mob>();
        if (mobScript != null)
        {
            mobScript.mobData = chosenMob;
            mobScript.InitializeMobAttributes();
        }
        else
        {
            Debug.LogError("Spawned enemy prefab does not have a Mob script attached!");
        }
    }

    int GetClosestSpawnPoint()
    {
        int closestIndex = -1;
        float shortestDistance = Mathf.Infinity;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            float distance = Vector3.Distance(player.position, spawnPoints[i].position);

            if (distance < shortestDistance && distance <= spawnRadius)
            {
                shortestDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }
}
