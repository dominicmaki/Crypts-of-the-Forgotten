using UnityEngine;
using UnityEngine.UI; // For using UI elements like Text
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform healthBar;  // Reference to the health bar transform
    [SerializeField] GameObject gameOverScreen; // Reference to the Game Over UI screen

    public float maxHealth = 100f;          // Maximum health
    [SerializeField] public float currentHealth;  // Current health

    void Start()
    {
        currentHealth = maxHealth * 1f; // Start with half health
        SetHealth(currentHealth);          // Set the initial health bar value

        // Hide the Game Over screen at the start
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    void Update()
    {
        // If health is 0 or less, trigger Game Over
        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    // Set health bar scale and show Game Over screen if health is 0
    public void SetHealth(float health)
    {
        health = Mathf.Clamp(health, 0f, maxHealth); // Clamp health between 0 and maxHealth
        float normalizedHealth = health / maxHealth;  // Normalize health value

        // Update the health bar scale
        healthBar.localScale = new Vector3(normalizedHealth, healthBar.localScale.y, 1);

        // Check for death
        if (health <= 0)
        {
            GameOver(); // Trigger Game Over if health is 0 or less
        }
    }

    // Call this method when you want to change the health
    public void UpdateHealth(float healthChange)
    {
        currentHealth += healthChange;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);  // Ensure health stays within range
        SetHealth(currentHealth);
    }

    // Handle game over logic
    private void GameOver()
    {
        SceneManager.LoadScene("DeadScene");
    }
}
