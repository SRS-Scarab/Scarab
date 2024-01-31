#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentType : ItemType
{
    public float maxHealth;
    public float attack;
    public float defence;
    
    public override void OnItemUse()
    {
        // todo swap currently active equipment in corresponding slot with this item
    }
}
