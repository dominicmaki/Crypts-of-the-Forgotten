using UnityEngine;
using UnityEngine.UI; // For UI elements like Text
using UnityEngine.SceneManagement;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform healthBar;  // Reference to the health bar transform
    [SerializeField] private GameObject gameOverScreen; // Reference to the Game Over UI screen
    [SerializeField] private TMP_Text healthText; // Reference to the health text (or TMP_Text for TextMeshPro)

    private PlayerStats playerStats; // Reference to PlayerStats script
    private int maxHealth;

    void Start()
    {
        // Get the PlayerStats instance
        playerStats = PlayerStats.Instance;

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats instance not found!");
            return;
        }

        // Initialize the health bar with the player's current health and max health
        UpdateHealthBar();
        
        // Hide the Game Over screen at the start
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    void Update()
    {
        // Continuously sync health bar with PlayerStats
        if (playerStats != null)
        {
            UpdateHealthBar();

            // Check if the player is dead
            if (playerStats.playerHP <= 0)
            {
                GameOver();
            }
        }
    }

    // Update the health bar and health text based on PlayerStats
    private void UpdateHealthBar()
    {
        // Ensure PlayerStats is valid
        if (playerStats == null) return;

        // Clamp health to avoid out-of-range values
        float currentHealth = Mathf.Clamp(playerStats.playerHP, 0, playerStats.maxHealth);

        // Calculate normalized health value
        float normalizedHealth = (float)currentHealth / playerStats.maxHealth;

        // Update the health bar's scale
        healthBar.localScale = new Vector3(normalizedHealth, healthBar.localScale.y, 1);

        // Update the health text
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{playerStats.maxHealth}";
        }
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;

        // Update the health bar display (in case it's needed at initialization)
        if (healthText != null)
        {
            healthText.text = $"{maxHealth}/{maxHealth}";
        }

        Debug.Log($"HealthBar: Max health set to {maxHealth}");
    }

    // Update current health
    public void SetHealth(int currentHealth)
    {
        // Ensure maxHealth is not zero or negative
        if (maxHealth <= 0)
        {
            Debug.LogError("HealthBar: maxHealth is zero or less. Cannot update health!");
            return;
        }

        // Clamp health between 0 and maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Normalize health (avoid division by zero)
        float normalizedHealth = (float)currentHealth / maxHealth;

        // Update the health bar scale
        healthBar.localScale = new Vector3(normalizedHealth, healthBar.localScale.y, 1);

        // Update the health text
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }

    // Handle game over logic
    private void GameOver()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true); // Show Game Over screen
        }

        // Optionally, delay the scene load or display a message
        SceneManager.LoadScene("DeadScene");
    }
}
