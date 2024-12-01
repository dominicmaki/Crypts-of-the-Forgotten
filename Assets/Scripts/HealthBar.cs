using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform healthBar;  // Reference to the health bar transform

    public float maxHealth = 100f;          // Maximum health
    [SerializeField] public float currentHealth;  // Current health

    void Start()
    {
        currentHealth = maxHealth * 0.5f; // Example: start with half health
        SetHealth(currentHealth);          // Set the initial health bar value
    }

    // Update is called once per frame
    void Update()
    {
        // Test: Decrease health for testing
        // Uncomment this line to test health decreasing
        // UpdateHealth(-0.1f);  // Uncomment this to see health bar change over time
    }

    public void SetHealth(float health)
    {
        // Make sure health stays between 0 and maxHealth
        health = Mathf.Clamp(health, 0f, maxHealth);
        
        // Normalize the health value to a value between 0 and 1 for the scale
        float normalizedHealth = health / maxHealth;

        // Update the health bar's scale
        healthBar.localScale = new Vector3(normalizedHealth, healthBar.localScale.y, 1);

        // Debugging: check the values
        Debug.Log("Health: " + health + ", Normalized Health: " + normalizedHealth);
    }

    // Call this method whenever you want to change the health
    public void UpdateHealth(float healthChange)
    {
        currentHealth += healthChange;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);  // Clamp the health
        SetHealth(currentHealth);
    }
}
