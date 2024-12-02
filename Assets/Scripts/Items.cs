using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Ring", order = 1)]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public int hpBonus;
    public int attackDamageBonus;
    public string description;
}
