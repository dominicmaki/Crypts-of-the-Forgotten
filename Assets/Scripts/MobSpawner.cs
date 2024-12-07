using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public MobSO[] mobVariants;     // Array of mob types (ScriptableObjects)
    public Transform[] spawnPoints; // Spawn points for enemies
    public float spawnInterval = 5f; // Time interval to spawn enemies

    void Start()
    {
        // Ensure mob variants are assigned before starting the spawn loop
        if (spawnPoints.Length > 0 && mobVariants.Length > 0)
        {
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval); // Start periodic spawning
        }
        else
        {
            Debug.LogError("Ensure spawn points and mob variants are assigned.");
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || mobVariants.Length == 0)
        {
            Debug.LogWarning("No spawn points or mob variants available.");
            return;
        }

        // Randomly choose a spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Randomly select a mob variant from the available mob variants (MobSO)
        MobSO chosenMob = mobVariants[Random.Range(0, mobVariants.Length)];

        // Instantiate the specific prefab for this mob variant
        GameObject spawnedMob = Instantiate(chosenMob.mobPrefab, spawnPoint.position, spawnPoint.rotation);

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
