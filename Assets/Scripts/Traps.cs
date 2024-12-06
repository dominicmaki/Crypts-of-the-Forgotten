using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.tag); // Log the tag
        
        // Check if the collider is the player
        if (other.CompareTag("Character"))
        {
            Debug.Log("Character triggered trap");

            // Access the PlayerStats instance
            PlayerStats playerStats = PlayerStats.Instance;

            if (playerStats != null)
            {
                // Apply damage using the TakeDamage method
                playerStats.TakeDamage(20);
            }
            else
            {
                Debug.LogError("PlayerStats instance not found!");
            }
        }
    }
}
