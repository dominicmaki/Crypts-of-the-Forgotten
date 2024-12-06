using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float projectileSpeed = 3f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform spawnTransformRight; // Spawn point for right-facing projectiles
    [SerializeField] Transform spawnTransformUp;    // Spawn point for up-facing projectiles
    [SerializeField] Transform spawnTransformDown;  // Spawn point for down-facing projectiles
    [SerializeField] float despawnTime = 4f;
    [SerializeField] AudioClip weaponSound; // Reference to the weapon sound to play

    private bool isFacingRight = true;
    private bool isFacingUp = false;
    private bool isFacingDown = false;

    private float timeSinceLastShot = 0f;
    private float fireRate = 0.2f;

    public PlayerStats playerStats;
    private AudioSource audioSource; // AudioSource for playing weapon sound

    void Start()
    {
        if (playerStats != null)
        {
            fireRate = playerStats.fireRate;
        }

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If no AudioSource is found, add one
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Sets the character's facing direction and flips the spawn point if necessary.
    /// </summary>
    public void SetFacingDirection(bool facingRight, bool facingUp, bool facingDown)
    {
        isFacingRight = facingRight;
        isFacingUp = facingUp;
        isFacingDown = facingDown;

        // Flip the horizontal spawn point for left/right direction
        if (!facingUp && !facingDown)
        {
            FlipSpawnTransformHorizontal();
        }
    }

    /// <summary>
    /// Flips the spawn transform for horizontal movement (left/right).
    /// </summary>
    private void FlipSpawnTransformHorizontal()
    {
        if (spawnTransformRight != null)
        {
            Vector3 localPos = spawnTransformRight.localPosition;
            localPos.x = Mathf.Abs(localPos.x) * (isFacingRight ? 1 : -1);
            spawnTransformRight.localPosition = localPos;
        }
    }

    /// <summary>
    /// Launches a projectile in the direction based on the character's facing direction.
    /// </summary>
    public void Launch()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= fireRate)
        {
            timeSinceLastShot = 0f;

            // Determine which spawn point to use
            Transform selectedSpawnTransform = spawnTransformRight; // Default to right-facing
            if (isFacingUp && spawnTransformUp != null) selectedSpawnTransform = spawnTransformUp;
            else if (isFacingDown && spawnTransformDown != null) selectedSpawnTransform = spawnTransformDown;

            // Calculate projectile direction towards the mouse
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector2 direction = (mousePosition - selectedSpawnTransform.position).normalized;

            // Instantiate the projectile
            GameObject newProjectile = Instantiate(projectilePrefab, selectedSpawnTransform.position, Quaternion.identity);

            // Assign damage to the projectile from player stats
            Projectile projectileScript = newProjectile.GetComponent<Projectile>();
            if (projectileScript != null && playerStats != null)
            {
                projectileScript.damage = playerStats.playerAttackDamage;
            }

            // Apply velocity to the projectile
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }

            // Destroy the projectile after a set time
            Destroy(newProjectile, despawnTime);

            // Play the weapon sound when the projectile is launched
            if (weaponSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(weaponSound);
            }
        }
    }
}
