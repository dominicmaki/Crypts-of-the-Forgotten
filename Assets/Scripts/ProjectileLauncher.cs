using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float projectileSpeed = 3f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform spawnTransform;
    [SerializeField] float despawnTime = 4f;

    private bool isFacingRight = true;

    // Reference to the PlayerStats singleton
    public PlayerStats playerStats;

    public void SetFacingDirection(bool facingRight)
    {
        if (isFacingRight != facingRight)
        {
            isFacingRight = facingRight;
            FlipSpawnTransform();
        }
    }

    private void FlipSpawnTransform()
    {
        Vector3 localPos = spawnTransform.localPosition;
        localPos.x = Mathf.Abs(localPos.x) * (isFacingRight ? 1 : -1); // Flip position across the x-axis
        spawnTransform.localPosition = localPos;
    }

    public void Launch()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure it's in the 2D plane

        // Calculate the direction from the spawn position to the mouse
        Vector2 direction = (mousePosition - spawnTransform.position).normalized;

        // Adjust direction based on character facing direction
        if (!isFacingRight)
        {
            direction.x = -Mathf.Abs(direction.x); // Ensure projectile direction respects facing
        }

        // Create and launch the projectile
        GameObject newProjectile = Instantiate(projectilePrefab, spawnTransform.position, Quaternion.identity);
        
        // Set the damage of the projectile to the player's attack damage
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();
        if (projectileScript != null && playerStats != null)
        {
            projectileScript.damage = playerStats.playerAttackDamage;  // Assign the player's attack damage to the projectile
        }

        newProjectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        // Destroy the projectile after some time
        Destroy(newProjectile, despawnTime);
    }
}
