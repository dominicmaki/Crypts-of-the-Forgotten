using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public PlayerStats playerStats; // Reference to PlayerStats script
    public TMP_Text hpText;         // Text component to display HP
    public TMP_Text attackText;     // Text component to display Attack Damage

    // UI slots for equipped items
    public Image ringSlotImage;

    private void Start()
    {
        if (playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }

        UpdateStatsUI();
        UpdateEquippedItemsUI();
    }

    public void UpdateStatsUI()
    {
        // Update player stats display
        hpText.text = $"HP: {playerStats.playerHP}";
        attackText.text = $"Attack Damage: {playerStats.playerAttackDamage}";
    }

    public void UpdateEquippedItemsUI()
    {
        // Check if the player has an equipped ring
        if (playerStats.equippedRing != null)
        {
            ringSlotImage.sprite = playerStats.equippedRing.itemSprite;
            ringSlotImage.gameObject.SetActive(true);
        }
        else
        {
            ringSlotImage.gameObject.SetActive(false);
        }
    }

    // Call this method to refresh the UI after equipping a new item
    public void RefreshUI()
    {
        UpdateStatsUI();
        UpdateEquippedItemsUI();
    }
}
