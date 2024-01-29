#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable")]
public class ConsumableType : ItemType
{
    public float health;
    public float mana;

    public override void OnItemUse()
    {
        // todo add health and mana to character
    }
}
