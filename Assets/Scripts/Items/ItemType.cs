#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemType : ScriptableObject
{
    public Sprite? icon;
    public string itemName = string.Empty;
    public string description = string.Empty;
    public float value;
    public float weight;
    public int stackSize;

    // todo add reference to calling inventory/player
    public virtual void OnItemUse()
    {
    }
}
