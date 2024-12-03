using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.tag);  // Log the tag
        
        // Check if the collider is the player
        if (other.CompareTag("Character"))
        {
            Debug.Log("Character triggered trap");

            // Access the PlayerStats instance
            PlayerStats playerStats = PlayerStats.Instance;

            if (playerStats != null)
            {
                // Apply damage to the player
                playerStats.playerHP -= 20;

                // Clamp health to avoid negative values
                playerStats.playerHP = Mathf.Clamp(playerStats.playerHP, 0, 100); // Replace 100 with the player's max health if dynamic

                Debug.Log("-20 health applied");
            }
            else
            {
                Debug.LogError("PlayerStats instance not found!");
            }
        }
    }
}
