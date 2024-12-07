using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public MobSO mobData;

    private int currentHealth;
    private PlayerStats playerStats;
    private Transform player;
    private Vector3 initialPosition; // Store the spawn position for patrolling

    public float patrolRadius = 10f; // Radius for patrolling around spawn point
    private float patrolCooldown = 0.5f; // Time interval between patrol movements
    private float lastPatrolTime;

    void Start()
    {
        if (mobData != null)
        {
            InitializeMobAttributes();
        }
        else
        {
            Debug.LogError("Mob data (MobSO) is not assigned!");
            return;
        }

        player = GameObject.FindWithTag("Character").transform;

        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }
        else
        {
            Debug.LogError("Player not found in the scene!");
        }

        initialPosition = transform.position; // Save spawn position
    }

    public void InitializeMobAttributes()
    {
        if (mobData != null)
        {
            currentHealth = mobData.maxHealth;
        }
    }

    void Update()
    {
        if (player == null || mobData == null) return;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= 10f)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer > 20f)
        {
            Despawn();
        }
        else
        {
            Patrol();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * mobData.moveSpeed * Time.deltaTime);
    }

    void Patrol()
{
    // If it's time for a patrol move
    if (Time.time - lastPatrolTime >= patrolCooldown)
    {
        // Pick a random position within the patrol radius around the initial position
        Vector3 randomDirection = Random.insideUnitCircle * patrolRadius; // Use 2D circle for patrol area
        Vector3 patrolTarget = initialPosition + new Vector3(randomDirection.x, randomDirection.y, 0);
        
        // Move towards the patrol target
        transform.position = Vector3.MoveTowards(transform.position, patrolTarget, mobData.moveSpeed * Time.deltaTime);

        // Update patrol time
        lastPatrolTime = Time.time;
    }
}


    void Despawn()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void AttackPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < 1.5f && playerStats != null)
        {
            int damage = mobData.attackDamage;
            playerStats.TakeDamage(damage);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Character"))
        {
            AttackPlayer();
        }
    }
}
