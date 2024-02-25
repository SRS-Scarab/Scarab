#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentType : ItemType
{
    public CombatStatsModifier modifier;
    
    public override void OnItemUse(CombatEntity playerEntity, Inventory inventory, int index)
    {
        // todo swap currently active equipment in corresponding slot with this item
    }
}
