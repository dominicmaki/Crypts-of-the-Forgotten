using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public MobSO[] mobVariants;      // Array of mob types (ScriptableObjects)
    public Transform[] spawnPoints;  // Spawn points for enemies
    [SerializeField] public float spawnRadius = 10f;  // Detection radius around each spawn point
    [SerializeField] public float spawnCooldown = 5f; // Time interval to prevent continuous spawning at a spawn point
    private float[] lastSpawnTime;   // Track last spawn time for each spawn point

    public Transform player;         // Reference to the player's transform

    void Start()
    {
        // Ensure mob variants and spawn points are assigned before starting
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
        // Loop through each spawn point and check if the player is within range
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            float distance = Vector3.Distance(player.position, spawnPoints[i].position);

            // If the player is within the spawn radius and the spawn cooldown has passed
            if (distance <= spawnRadius && Time.time - lastSpawnTime[i] >= spawnCooldown)
            {
                // Spawn an enemy at this spawn point
                SpawnEnemy(i);

                // Reset the last spawn time to prevent continuous spawning
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

        // Randomly select a mob variant from the available mob variants
        MobSO chosenMob = mobVariants[Random.Range(0, mobVariants.Length)];

        // Instantiate the specific prefab for this mob variant
        GameObject spawnedMob = Instantiate(chosenMob.mobPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        // Retrieve the Mob script attached to the newly spawned enemy
        Mob mobScript = spawnedMob.GetComponent<Mob>();

        if (mobScript != null)
        {
            // Assign the MobSO data to the mob's Mob script
            mobScript.mobData = chosenMob;

            // Initialize mob attributes based on the selected MobSO
            mobScript.InitializeMobAttributes();
        }
        else
        {
            Debug.LogError("Spawned enemy prefab does not have a Mob script attached!");
        }
    }
}
