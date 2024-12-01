using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance; // Singleton instance

    public int playerHP = 100;
    public int playerAttackDamage = 10;

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

