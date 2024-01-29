#nullable enable
using System;

[Serializable]
public struct ItemStack
{
    public ItemType? itemType;
    public int quantity;

    public ItemStack(ItemType itemType, int quantity)
    {
        this.itemType = itemType;
        this.quantity = quantity;
    }

    public ItemStack(ItemStack other)
    {
        itemType = other.itemType;
        quantity = other.quantity;
    }
}
