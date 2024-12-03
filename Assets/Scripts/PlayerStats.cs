using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance; // Singleton instance

    public int playerHP = 100;            // Current health
    public int maxHealth = 100;           // Maximum health
    public int playerAttackDamage = 10;   // Base attack damage

    // Equipped items
    public ItemSO equippedRing1; // First ring
    public ItemSO equippedRing2; // Second ring
    public ItemSO equippedWand;  // Wand
    public ItemSO equippedCloak; // Cloak

    // Total bonuses from all items
    public int totalHealthBonus = 0;  // Total health bonus from all items
    public int totalAttackDamageBonus = 0;  // Total attack damage bonus from all items

    private HealthBar healthBar; // Reference to the HealthBar

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Find the HealthBar component in the scene
        healthBar = FindObjectOfType<HealthBar>();

        // Initialize the health bar with the player's current HP
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth); // Set the maximum health
            healthBar.SetHealth(playerHP);      // Set the current health
        }

        // Initialize player stats
        UpdateStats();
    }

    // Method to equip an item
    public void EquipItem(ItemSO item)
    {
        if (item == null) return;

        // Update total bonuses when an item is equipped
        if (item.hpBonus > 0) totalHealthBonus += item.hpBonus;
        if (item.attackDamageBonus > 0) totalAttackDamageBonus += item.attackDamageBonus;

        // Equip item to the appropriate slot
        switch (item.itemType)
        {
            case ItemType.Ring:
                if (equippedRing1 == null)
                {
                    equippedRing1 = item;
                    Debug.Log($"Equipped in Ring Slot 1: {item.itemName}");
                }
                else if (equippedRing2 == null)
                {
                    equippedRing2 = item;
                    Debug.Log($"Equipped in Ring Slot 2: {item.itemName}");
                }
                else
                {
                    Debug.LogWarning("Both ring slots are already occupied!");
                }
                break;

            case ItemType.Wand:
                if (equippedWand == null)
                {
                    equippedWand = item;
                    Debug.Log($"Equipped Wand: {item.itemName}");
                }
                else
                {
                    Debug.LogWarning("Wand slot is already occupied!");
                }
                break;

            case ItemType.Cloak:
                if (equippedCloak == null)
                {
                    equippedCloak = item;
                    Debug.Log($"Equipped Cloak: {item.itemName}");
                }
                else
                {
                    Debug.LogWarning("Cloak slot is already occupied!");
                }
                break;

            default:
                Debug.LogWarning("Invalid item type!");
                break;
        }

        // Update stats after equipping the item
        UpdateStats();
    }

    private void UpdateStats()
    {
        // Update max health and attack damage with the total bonuses
        maxHealth = 100 + totalHealthBonus;  // Assume base health is 100 (you can adjust accordingly)
        playerAttackDamage = 10 + totalAttackDamageBonus;  // Assume base damage is 10 (you can adjust accordingly)

        // Scale current health to match the new maximum health
        float healthPercentage = (float)playerHP / maxHealth;
        playerHP = Mathf.RoundToInt(healthPercentage * maxHealth);

        // Update the health bar
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth); // Update the max health on the health bar
            healthBar.SetHealth(playerHP);     // Update the current health on the health bar
        }

        Debug.Log($"Stats updated: HP {playerHP}/{maxHealth}, Attack {playerAttackDamage}");
    }

    // Method to unequip an item
    public void UnequipItem(ItemSO item)
    {
        if (item == null) return;

        // Update total bonuses when an item is unequipped
        if (item.hpBonus > 0) totalHealthBonus -= item.hpBonus;
        if (item.attackDamageBonus > 0) totalAttackDamageBonus -= item.attackDamageBonus;

        // Unequip item from the appropriate slot
        switch (item.itemType)
        {
            case ItemType.Ring:
                if (equippedRing1 == item)
                {
                    equippedRing1 = null;
                    Debug.Log($"Unequipped from Ring Slot 1: {item.itemName}");
                }
                else if (equippedRing2 == item)
                {
                    equippedRing2 = null;
                    Debug.Log($"Unequipped from Ring Slot 2: {item.itemName}");
                }
                break;

            case ItemType.Wand:
                if (equippedWand == item)
                {
                    equippedWand = null;
                    Debug.Log($"Unequipped Wand: {item.itemName}");
                }
                break;

            case ItemType.Cloak:
                if (equippedCloak == item)
                {
                    equippedCloak = null;
                    Debug.Log($"Unequipped Cloak: {item.itemName}");
                }
                break;
        }

        // Update stats after unequipping the item
        UpdateStats();
    }

    // Public methods to get total bonuses
    public int GetTotalHealthBonus() => totalHealthBonus;
    public int GetTotalAttackDamageBonus() => totalAttackDamageBonus;

    // Method to handle damage taken by the player
    public void TakeDamage(int damage)
    {
        playerHP -= damage;
        playerHP = Mathf.Max(0, playerHP);  // Ensure health doesn't go below 0

        // Update the health bar after damage
        if (healthBar != null)
        {
            healthBar.SetHealth(playerHP);
        }

        if (playerHP <= 0)
        {
            Die();
        }
    }

    // Method to handle player death
    void Die()
    {
        // Handle player death (e.g., play death animation, restart level, etc.)
        Debug.Log("Player has died!");
    }
}
