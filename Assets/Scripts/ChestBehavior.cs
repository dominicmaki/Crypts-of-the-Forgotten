using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestBehavior : MonoBehaviour
{
    public Sprite openChestSprite;  // Assign the opened chest sprite in the Inspector
    public Sprite closedChestSprite; // Assign the closed chest sprite in the Inspector
    public GameObject chestContents; // Assign the Reward UI GameObject in the Inspector
    public TMP_Text itemNameText;        // Text to display the item name
    public TMP_Text itemStatsText;       // Text to display the item stats
    public Image itemImage;          // Image to display the item sprite
    private SpriteRenderer spriteRenderer;

    private PlayerStats playerStats;
    private bool isOpened = false;   // Track whether the chest is open or closed

    // Define the item to display in the chest
    public Items itemInChest; // The item inside the chest (set in the Inspector)

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    void OnMouseDown()
    {
        if (isOpened)
        {
            CloseChest(); // If chest is open, close it
        }
        else
        {
            OpenChest(); // If chest is closed, open it
        }
    }

    void OpenChest()
    {
        // Change the sprite to the open chest
        spriteRenderer.sprite = openChestSprite;

        // Show the chest contents UI
        if (chestContents != null)
        {
            chestContents.SetActive(true);

            // Update the image and text for the item
            if (itemInChest != null)
            {
                // Set the item sprite and name
                itemImage.sprite = itemInChest.itemSprite;
                itemNameText.text = itemInChest.itemName;

                // Set the item stats
                itemStatsText.text = $"HP Bonus: {itemInChest.hpBonus}\nAttack Bonus: {itemInChest.attackDamageBonus}";
            }
        }

        isOpened = true; // Mark the chest as opened
    }

    void CloseChest()
    {
        // Change the sprite back to the closed chest
        spriteRenderer.sprite = closedChestSprite;

        // Hide the chest contents UI
        if (chestContents != null)
        {
            chestContents.SetActive(false);
        }

        isOpened = false; // Mark the chest as closed
    }

    // Equip the item when clicked
    public void EquipItem()
    {
        // Ensure playerStats is assigned
        if (playerStats != null)
        {
            // Equip the item (add bonuses to player stats)
            playerStats.playerHP += itemInChest.hpBonus;
            playerStats.playerAttackDamage += itemInChest.attackDamageBonus;

            Debug.Log("Item equipped: " + itemInChest.itemName);
        }
        else
        {
            Debug.LogError("PlayerStats not assigned!");
        }

        // Optionally, hide the chest UI after equipping the item
        chestContents.SetActive(false);
    }

}
