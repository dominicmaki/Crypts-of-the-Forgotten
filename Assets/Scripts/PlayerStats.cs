using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance; // Singleton instance

    public int playerHP = 100;
    public int playerAttackDamage = 10;

    // Equipped items
    public ItemSO equippedRing1; // First ring
    public ItemSO equippedRing2; // Second ring
    public ItemSO equippedWand;  // Wand
    public ItemSO equippedCloak; // Cloak

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EquipItem(ItemSO item)
    {
        if (item == null) return;

        switch (item.itemType)
        {
            case ItemType.Ring:
                if (equippedRing1 == null)
                {
                    equippedRing1 = item;
                    ApplyItemStats(item);
                    Debug.Log($"Equipped in Ring Slot 1: {item.itemName}");
                }
                else if (equippedRing2 == null)
                {
                    equippedRing2 = item;
                    ApplyItemStats(item);
                    Debug.Log($"Equipped in Ring Slot 2: {item.itemName}");
                }
                else
                {
                    Debug.LogWarning("Both ring slots are already occupied!");
                }
                break;

            case ItemType.Wand:
                if (equippedWand == null)
                {
                    equippedWand = item;
                    ApplyItemStats(item);
                    Debug.Log($"Equipped Wand: {item.itemName}");
                }
                else
                {
                    Debug.LogWarning("Wand slot is already occupied!");
                }
                break;

            case ItemType.Cloak:
                if (equippedCloak == null)
                {
                    equippedCloak = item;
                    ApplyItemStats(item);
                    Debug.Log($"Equipped Cloak: {item.itemName}");
                }
                else
                {
                    Debug.LogWarning("Cloak slot is already occupied!");
                }
                break;

            default:
                Debug.LogWarning("Invalid item type!");
                break;
        }
    }

    private void ApplyItemStats(ItemSO item)
    {
        playerHP += item.hpBonus;
        playerAttackDamage += item.attackDamageBonus;
        Debug.Log($"Stats updated: HP {playerHP}, Attack {playerAttackDamage}");
    }

    private void RemoveItemStats(ItemSO item)
    {
        playerHP -= item.hpBonus;
        playerAttackDamage -= item.attackDamageBonus;
        Debug.Log($"Stats updated: HP {playerHP}, Attack {playerAttackDamage}");
    }

    public void UnequipItem(ItemSO item)
    {
        if (item == null) return;

        switch (item.itemType)
        {
            case ItemType.Ring:
                if (equippedRing1 == item)
                {
                    equippedRing1 = null;
                    RemoveItemStats(item);
                    Debug.Log($"Unequipped from Ring Slot 1: {item.itemName}");
                }
                else if (equippedRing2 == item)
                {
                    equippedRing2 = null;
                    RemoveItemStats(item);
                    Debug.Log($"Unequipped from Ring Slot 2: {item.itemName}");
                }
                break;

            case ItemType.Wand:
                if (equippedWand == item)
                {
                    equippedWand = null;
                    RemoveItemStats(item);
                    Debug.Log($"Unequipped Wand: {item.itemName}");
                }
                break;

            case ItemType.Cloak:
                if (equippedCloak == item)
                {
                    equippedCloak = null;
                    RemoveItemStats(item);
                    Debug.Log($"Unequipped Cloak: {item.itemName}");
                }
                break;
        }
    }
}
