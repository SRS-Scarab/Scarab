#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemType : ScriptableObject
{
    public ItemCategory? category;
    public Sprite? icon;
    public string itemName = string.Empty;
    public string description = string.Empty;
    public float value;
    public float weight;
    public int stackSize;

    public virtual void OnItemUse(CombatEntity playerEntity, Inventory inventory, int index)
    {
    }
}
