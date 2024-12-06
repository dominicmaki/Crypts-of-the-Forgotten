using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestBehavior : MonoBehaviour
{
    public Sprite openChestSprite;  // Sprite for the opened chest
    public Sprite closedChestSprite; // Sprite for the closed chest
    public GameObject chestContents; // Reference to chest contents (already in the scene)

    private TMP_Text itemNameText;        // Text to display the item name
    private TMP_Text itemStatsText;       // Text to display the item stats
    private TMP_Text itemDescriptionText; // Text to display the item description
    private Image itemImage;          // Image to display the item sprite

    private SpriteRenderer spriteRenderer;
    public ItemSO[] possibleItems;
    private ItemSO selectedItem;
    public InventoryUI inventoryUI;
    private PlayerStats playerStats;
    private bool isOpened = false;   // Track whether the chest is open or closed

    public Button equipButton; // Reference to the equip button (for dynamic interaction)

    // Audio variables
    public AudioClip sound;  // Sound for opening the chest
    private AudioSource audioSource; // Reference to AudioSource component

    void Start()
{
    spriteRenderer = GetComponent<SpriteRenderer>();
    playerStats = FindObjectOfType<PlayerStats>();

    // Try to get the AudioSource component
    audioSource = GetComponent<AudioSource>();
    
    // Check if audioSource is missing and add a default AudioSource if necessary
    if (audioSource == null)
    {
        Debug.LogWarning("AudioSource component not found. Adding AudioSource component.");
        audioSource = gameObject.AddComponent<AudioSource>();  // Add AudioSource if missing
    }

    // Get references to UI components from chestContents in the hierarchy
    itemImage = chestContents.transform.Find("itemImage").GetComponent<Image>();
    itemNameText = chestContents.transform.Find("itemName").GetComponent<TMP_Text>();
    itemStatsText = chestContents.transform.Find("itemStats").GetComponent<TMP_Text>();
    itemDescriptionText = chestContents.transform.Find("itemDescriptionText").GetComponent<TMP_Text>();

    // Get the equip button and assign the EquipItem method
    equipButton.onClick.AddListener(EquipItem);

    // Randomly select an item from the list
    selectedItem = possibleItems[Random.Range(0, possibleItems.Length)];

    // Update UI with the selected item's details
    UpdateItemUI();

    // Initially hide the chest contents
    chestContents.SetActive(false);

    // Debugging to check if Start() is being called
    Debug.Log("Start() called - Audio should not play yet.");
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
    chestContents.SetActive(true);
    UpdateItemUI(); // Update the UI with item details

    // Play the open chest sound
    if (audioSource != null && sound != null)
    {
        audioSource.PlayOneShot(sound); // Use PlayOneShot to play sound once
    }

    isOpened = true; // Mark the chest as opened
}

void CloseChest()
{
    // Change the sprite back to the closed chest
    spriteRenderer.sprite = closedChestSprite;

    // Hide the chest contents UI
    chestContents.SetActive(false);

    // Play the close chest sound
    if (audioSource != null && sound != null)
    {
        audioSource.PlayOneShot(sound); // Play sound when closing the chest
    }

    isOpened = false; // Mark the chest as closed
}



    void UpdateItemUI()
    {
        if (selectedItem != null)
        {
            itemImage.sprite = selectedItem.itemSprite;
            itemNameText.text = selectedItem.itemName;
            itemStatsText.text = $"HP Bonus: {selectedItem.hpBonus}\nAttack Bonus: {selectedItem.attackDamageBonus}";
            itemDescriptionText.text = selectedItem.description; // Display the description
        }
    }

    // Equip the item when clicked
    public void EquipItem()
    {
        if (selectedItem != null && PlayerStats.Instance != null)
        {
            // Equip the item
            PlayerStats.Instance.EquipItem(selectedItem);

            // Update the Inventory UI
            if (inventoryUI != null)
            {
                inventoryUI.UpdateUI();
            }

            // Hide chest contents UI after equipping
            chestContents.SetActive(false);
        }
        else
        {
            Debug.LogError("PlayerStats not assigned or no item selected!");
        }
    }
}
