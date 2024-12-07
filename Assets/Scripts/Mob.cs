using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public MobSO mobData;  // Reference to the Mob ScriptableObject (for mob variation)

    private int currentHealth;  // Current health
    private PlayerStats playerStats; // Reference to the PlayerStats component
    private Transform player;  // Reference to the player's position (to follow or attack)

    void Start()
    {
        // If mobData is assigned, initialize attributes
        if (mobData != null)
        {
            InitializeMobAttributes();
        }
        else
        {
            Debug.LogError("Mob data (MobSO) is not assigned!");
            return;
        }

        // Automatically find the player in the scene
        player = GameObject.FindWithTag("Character").transform;

        // Ensure the player object is found before trying to get the PlayerStats component
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }
        else
        {
            Debug.LogError("Player not found in the scene!");
        }
    }

    // Initialize or reset mob attributes based on the MobSO
    public void InitializeMobAttributes()
    {
        if (mobData != null)
        {
            currentHealth = mobData.maxHealth; // Set initial health from the ScriptableObject
        }
    }

    void Update()
    {
        // Basic behavior: Move towards the player
        if (player != null && mobData != null)
        {
            MoveTowardsPlayer();
        }

        // Optionally, check if the enemy is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Move the enemy towards the player
    void MoveTowardsPlayer()
    {
        if (mobData != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * mobData.moveSpeed * Time.deltaTime);
        }
    }

    // Deal damage to the enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the enemy's health is zero or less
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Handle enemy death
    void Die()
    {
        // Optionally, play a death animation or effect here

        // Destroy the enemy object
        Destroy(gameObject);

        // Play death sound (if available in MobSO)
        /*
        if (mobData != null && mobData.deathSound != null)
        {
            AudioSource.PlayClipAtPoint(mobData.deathSound, transform.position);
        }*/
    }

    // Optionally, you can make the enemy attack the player if they are close enough
    public void AttackPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < 1.5f && playerStats != null)
        {
            // Get the player's damage value (considering item bonuses)
            int damage = mobData.attackDamage;  // Use attackDamage from MobSO
            playerStats.TakeDamage(damage); // Apply the damage to the player
        }
    }

    // Handle collision with projectiles and player
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Mob takes damage from the projectile
            TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
            Destroy(collision.gameObject);  // Destroy the projectile on impact
        }

        // Check if the Mob collides with the player
        if (collision.gameObject.CompareTag("Character"))
        {
            // Mob deals damage to the player (using MobSO attack damage)
            AttackPlayer();
        }
    }
}
