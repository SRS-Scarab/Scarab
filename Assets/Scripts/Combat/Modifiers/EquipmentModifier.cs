#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Equipment")]
public class EquipmentModifier : AggregateModifier
{
    [SerializeField] private InventoryVariable? equipmentVar;

    public override CombatStats Modify(CombatStats stats)
    {
        Clear();
        
        if (equipmentVar != null)
        {
            for (var i = 0; i < equipmentVar.Provide().GetMaxSlots(); i++)
            {
                var stack = equipmentVar.Provide().GetStack(i);
                if (stack.itemType != null && stack.itemType is EquipmentType equipment)
                {
                    Add(equipment.modifier);
                }
            }
        }

        return base.Modify(stats);
    }
}
