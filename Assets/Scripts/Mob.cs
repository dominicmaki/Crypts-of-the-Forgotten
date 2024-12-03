using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public int maxHealth = 50;  // Maximum health
    public int currentHealth;   // Current health
    public int attackDamage = 5; // Damage dealt to the player
    public float moveSpeed = 2f;  // Movement speed
    public Transform player;    // Reference to the player's position (to follow or attack)
    private PlayerStats playerStats; // Reference to the PlayerStats component

    void Start()
    {
        currentHealth = maxHealth;
        // Get the PlayerStats component from the player object
        playerStats = player.GetComponent<PlayerStats>();
    }

    void Update()
    {
        // Basic behavior: Move towards the player
        if (player != null)
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
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
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
    }

    // Optionally, you can make the enemy attack the player if they are close enough
    public void AttackPlayer()
{
    if (Vector3.Distance(transform.position, player.position) < 1.5f)
    {
        // Get the player's damage value (considering item bonuses)
        int damage = playerStats.playerAttackDamage;
        playerStats.TakeDamage(damage); // Apply the damage to the player
    }
}

    // Handle collision with projectiles
    void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Projectile"))
    {
        Debug.Log("Projectile hit the Mob!");
        TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
        Destroy(collision.gameObject);  // Destroy the projectile on impact
    }
}

}

