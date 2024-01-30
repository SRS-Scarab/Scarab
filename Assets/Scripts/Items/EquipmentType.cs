#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentType : ItemType
{
    public float armor;
    public float health;

    public override void OnItemUse()
    {
        // todo swap currently active equipment in corresponding slot with this item
    }
}
