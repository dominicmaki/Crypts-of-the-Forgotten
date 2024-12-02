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
    public TMP_Text itemDescriptionText; // Text to display the item description
    public Image itemImage;          // Image to display the item sprite
    private SpriteRenderer spriteRenderer;
    public ItemSO[] possibleItems;
    private ItemSO selectedItem;

    private PlayerStats playerStats;
    private bool isOpened = false;   // Track whether the chest is open or closed

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStats = FindObjectOfType<PlayerStats>();

        // Randomly select an item from the list
        selectedItem = possibleItems[Random.Range(0, possibleItems.Length)];

        // Update UI with the selected item's details
        if (selectedItem != null)
        {
            itemImage.sprite = selectedItem.itemSprite;
            itemNameText.text = selectedItem.itemName;
            itemStatsText.text = $"HP Bonus: {selectedItem.hpBonus}\nAttack Bonus: {selectedItem.attackDamageBonus}";
            itemDescriptionText.text = selectedItem.description; // Display the description
        }
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

            // Update the image, name, stats, and description for the item
            if (selectedItem != null)
            {
                // Set the item sprite and name
                itemImage.sprite = selectedItem.itemSprite;
                itemNameText.text = selectedItem.itemName;

                // Set the item stats
                itemStatsText.text = $"HP Bonus: {selectedItem.hpBonus}\nAttack Bonus: {selectedItem.attackDamageBonus}";

                // Set the item description
                itemDescriptionText.text = selectedItem.description;
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
            playerStats.playerHP += selectedItem.hpBonus;
            playerStats.playerAttackDamage += selectedItem.attackDamageBonus;

            Debug.Log("Item equipped: " + selectedItem.itemName);
        }
        else
        {
            Debug.LogError("PlayerStats not assigned!");
        }

        // Hide chest contents UI after equipping
        chestContents.SetActive(false);
    }
}
