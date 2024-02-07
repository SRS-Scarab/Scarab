#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable")]
public class ConsumableType : ItemType
{
    public float health;
    public float mana;

    public override void OnItemUse(CombatEntity playerEntity, Inventory inventory, int index)
    {
        playerEntity.ProcessHeal(health, mana);
        var stack = inventory.GetStack(index);
        stack.quantity--;
        inventory.SetStack(index, stack.quantity == 0 ? new ItemStack() : stack);
    }
}
