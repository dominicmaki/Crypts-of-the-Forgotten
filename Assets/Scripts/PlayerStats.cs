using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance; // Singleton instance

    public int playerHP = 100;
    public int playerAttackDamage = 10;

     public ItemSO equippedRing; // Reference to the currently equipped ring

    public void EquipItem(ItemSO item)
    {
        if (item != null)
        {
            // Update stats based on the item
            playerHP += item.hpBonus;
            playerAttackDamage += item.attackDamageBonus;

            // Check the item type
            if (item.itemType == ItemType.Ring)
            {
                equippedRing = item; // Equip the ring
            }

            Debug.Log($"Equipped: {item.itemName}");
        }
    }

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

    // Method to increase the player's HP
    public void IncreaseHP(int amount)
    {
        playerHP += amount;
        Debug.Log("New HP: " + playerHP);
    }

    // Method to increase the player's attack damage
    public void IncreaseAttackDamage(int amount)
    {
        playerAttackDamage += amount;
        Debug.Log("New Attack Damage: " + playerAttackDamage);
    }
}

