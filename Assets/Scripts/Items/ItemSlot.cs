#nullable enable
using System;

[Serializable]
public class ItemSlot
{
    public ItemCategory? filter;
    public ItemStack stack;

    public bool CanAccept(ItemStack query) => query.IsValid() && (filter == null || query.itemType == null || query.itemType.category == filter);
}
