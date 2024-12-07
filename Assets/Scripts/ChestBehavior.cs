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
    audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

    // Get references to UI components from chestContents in the hierarchy
    itemImage = chestContents.transform.Find("itemImage").GetComponent<Image>();
    itemNameText = chestContents.transform.Find("itemName").GetComponent<TMP_Text>();
    itemStatsText = chestContents.transform.Find("itemStats").GetComponent<TMP_Text>();
    itemDescriptionText = chestContents.transform.Find("itemDescriptionText").GetComponent<TMP_Text>();
    equipButton.onClick.RemoveAllListeners(); // Remove previous listeners
    equipButton.onClick.AddListener(() => EquipItem()); // Add the current chest's listener


    // Randomly select an item from the list
    if (possibleItems != null && possibleItems.Length > 0)
    {
        selectedItem = possibleItems[Random.Range(0, possibleItems.Length)];
        Debug.Log($"Chest '{name}' selected item: {selectedItem.itemName}");
    }
    else
    {
        Debug.LogWarning($"Chest '{name}' has no possible items!");
    }

    UpdateItemUI();
    chestContents.SetActive(false);
}



void OnMouseDown()
{
    if (isOpened)
    {
        // If the chest is open, close it on click
        CloseChest();
    }
    else
    {
        // Check if any other chest is open and close it
        PlayerInteractingWithAnotherChest();

        // Open this chest
        OpenChest();
    }
}


// Helper method
bool PlayerInteractingWithAnotherChest()
{
    // Find all chests in the scene
    ChestBehavior[] allChests = FindObjectsOfType<ChestBehavior>();
    foreach (var chest in allChests)
    {
        if (chest != this && chest.isOpened) // Close other open chests
        {
            chest.CloseChest();
        }
    }
    return false; // Always allow the current chest to proceed
}



void OpenChest()
{
    if (isOpened)
    {
        Debug.Log($"Chest '{name}' is already open.");
        return;
    }

    // Ensure the current chest context
    Debug.Log($"Opening chest '{name}' with item '{selectedItem.itemName}'");
    chestContents.SetActive(true);
    spriteRenderer.sprite = openChestSprite;
    isOpened = true;

    // Reset listeners for the equip button
    equipButton.onClick.RemoveAllListeners(); // Remove old listeners
    equipButton.onClick.AddListener(() =>
    {
        Debug.Log($"Equipping item '{selectedItem.itemName}' from chest '{name}'");
        EquipItem();
    });

    if (audioSource != null && sound != null)
    {
        audioSource.PlayOneShot(sound);
    }
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
        // Debug the selected item
        Debug.Log($"Equipping item from chest '{name}': {selectedItem.itemName}");

        // Equip the item
        PlayerStats.Instance.EquipItem(selectedItem);

        // Update the Inventory UI
        if (inventoryUI != null)
        {
            inventoryUI.UpdateUI();
        }

        // Hide chest contents UI after equipping
        chestContents.SetActive(false);

        // Mark chest as opened to prevent re-equipping
        isOpened = true;
    }
    else
    {
        Debug.LogError($"PlayerStats not assigned or no item selected for chest '{name}'!");
    }
}

}
