using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab;          // Prefab for the chest
    public ItemSO[] possibleItems1;         // Possible items for chest 1
    public ItemSO[] possibleItems2;         // Possible items for chest 2
    public Transform[] spawnPoints;         // Array of spawn points

    // Reference to chest contents GameObject (UI element)
    public GameObject chestContentsPrefab;  // Chest contents prefab or UI element to assign

    void Start()
    {
        SpawnChest(spawnPoints[0].position, spawnPoints[0].rotation, possibleItems1);
        SpawnChest(spawnPoints[1].position, spawnPoints[1].rotation, possibleItems2);
    }

    void SpawnChest(Vector3 position, Quaternion rotation, ItemSO[] items)
    {
        GameObject chest = Instantiate(chestPrefab, position, rotation);

        // Get the ChestBehavior component
        ChestBehavior chestBehavior = chest.GetComponent<ChestBehavior>();

        // Check if ChestBehavior is found and assign the chestContents
        if (chestBehavior != null)
        {
            chestBehavior.possibleItems = items;

            // Manually assign chestContents for the spawned chest
            chestBehavior.chestContents = chestContentsPrefab;  // Assign the prefab or UI object here
        }
    }
}
