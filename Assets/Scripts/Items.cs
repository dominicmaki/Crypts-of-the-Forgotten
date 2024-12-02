using UnityEngine;

public enum ItemType
{
    Ring,
    Wand,
    Cloak
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Ring", order = 1)]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public int hpBonus;
    public int attackDamageBonus;
    public string description;
    public ItemType itemType; 

}
