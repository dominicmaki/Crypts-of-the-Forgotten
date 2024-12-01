// Item.cs
using UnityEngine;

[System.Serializable]
public class Items
{
    public string itemName;  // Name of the item (e.g., "Ring of Power")
    public Sprite itemSprite;  // Icon for the item (e.g., image of the ring)
    public int hpBonus;  // HP bonus provided by the item
    public int attackDamageBonus;  // Attack damage bonus provided by the item

    // Constructor to create an item
    public Items(string name, Sprite sprite, int hp, int attackDamage)
    {
        itemName = name;
        itemSprite = sprite;
        hpBonus = hp;
        attackDamageBonus = attackDamage;
    }
}
