using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MobSpawner : MonoBehaviour
{
    public MobSO[] mobVariants;      
    public Transform[] spawnPoints;  
    [SerializeField] public float spawnRadius = 20f;
    [SerializeField] public float spawnCooldown = 3f;
    private float[] lastSpawnTime;

    public Transform player;
    private Dictionary<string, int> activeMobs = new Dictionary<string, int>(); // Track active mob counts
    private int maxActiveMobs = 10; // Max active mobs at any time

    void Start()
    {
        if (spawnPoints.Length > 0 && mobVariants.Length > 0)
        {
            lastSpawnTime = new float[spawnPoints.Length];
        }
        else
        {
            Debug.LogError("Ensure spawn points and mob variants are assigned.");
        }
    }

    void Update()
    {
        int totalActiveMobs = activeMobs.Values.Sum();
        if (totalActiveMobs < maxActiveMobs)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                float distance = Vector3.Distance(player.position, spawnPoints[i].position);

                if (distance <= spawnRadius && Time.time - lastSpawnTime[i] >= spawnCooldown)
                {
                    SpawnEnemy(i);
                    lastSpawnTime[i] = Time.time;
                }
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

        MobSO chosenMob = mobVariants[Random.Range(0, mobVariants.Length)];

        // Spawn mob and ensure variety
        GameObject spawnedMob = Instantiate(chosenMob.mobPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        Mob mobScript = spawnedMob.GetComponent<Mob>();
        if (mobScript != null)
        {
            mobScript.mobData = chosenMob;
            mobScript.InitializeMobAttributes();
            if (!activeMobs.ContainsKey(chosenMob.mobPrefab.name))
            {
                activeMobs[chosenMob.mobPrefab.name] = 0; // Initialize count if it's the first spawn
            }
            activeMobs[chosenMob.mobPrefab.name]++; // Increment active mob count
        }
        else
        {
            Debug.LogError("Spawned enemy prefab does not have a Mob script attached!");
        }
    }

    // Reset mob count when a mob is destroyed
    public void OnMobDestroyed(string mobName)
    {
        if (activeMobs.ContainsKey(mobName))
        {
            activeMobs[mobName]--;
        }
    }
}
