#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentType : ItemType
{
    public EquipmentSlot equipmentSlot;
    public float armor;
    public float health;

    public override void OnItemUse()
    {
        // todo swap currently active equipment in corresponding slot with this item
    }
}

public enum EquipmentSlot
{
    Helmet,
    Chestplate,
    Leggings,
    Boots,
    Necklace,
    Armband,
    Earrings
}