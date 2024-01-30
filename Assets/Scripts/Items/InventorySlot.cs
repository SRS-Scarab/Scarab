#nullable enable
using System;

[Serializable]
public class InventorySlot
{
    public ItemCategory? filter;
    public ItemStack stack;

    public InventorySlot()
    {
    }

    public InventorySlot(ItemCategory? category, ItemStack data)
    {
        filter = category;
        stack = data;
    }
    
    public bool CanAccept(ItemStack query) => query.IsValid() && (filter == null || query.itemType == null || query.itemType.category == filter);
}
