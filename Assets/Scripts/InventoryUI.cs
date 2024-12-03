using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public TMP_Text ring1Text;
    public TMP_Text ring2Text;
    public TMP_Text wandText;
    public TMP_Text cloakText;
    public TMP_Text statsText;

    public Image ring1Image;
    public Image ring2Image;
    public Image wandImage;
    public Image cloakImage;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = PlayerStats.Instance;
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Ring 1
        if (playerStats.equippedRing1 != null)
        {
            ring1Text.text = playerStats.equippedRing1.itemName;
            ring1Image.sprite = playerStats.equippedRing1.itemSprite;
            ring1Image.enabled = true; // Show the sprite
        }
        else
        {
            // ring1Text.text = "Empty";
            ring1Image.enabled = false; // Hide the sprite
        }

        // Ring 2
        if (playerStats.equippedRing2 != null)
        {
            ring2Text.text = playerStats.equippedRing2.itemName;
            ring2Image.sprite = playerStats.equippedRing2.itemSprite;
            ring2Image.enabled = true;
        }
        else
        {
            // ring2Text.text = "Empty";
            ring2Image.enabled = false;
        }

        // Wand
        if (playerStats.equippedWand != null)
        {
            wandText.text = playerStats.equippedWand.itemName;
            wandImage.sprite = playerStats.equippedWand.itemSprite;
            wandImage.enabled = true;
        }
        else
        {
            // wandText.text = "Empty";
            wandImage.enabled = false;
        }

        // Cloak
        if (playerStats.equippedCloak != null)
        {
            cloakText.text = playerStats.equippedCloak.itemName;
            cloakImage.sprite = playerStats.equippedCloak.itemSprite;
            cloakImage.enabled = true;
        }
        else
        {
            // cloakText.text = "Empty";
            cloakImage.enabled = false;
        }

        // Stats
        statsText.text = $"HP Bonus: {playerStats.totalHealthBonus}\nATK Bonus: {playerStats.totalAttackDamageBonus} +10";
    }
}
