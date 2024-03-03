#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Inventory
{
    [SerializeField] private List<InventorySlot> slots = new();
    
    public Inventory()
    {
    }

    public Inventory(int maxSlots)
    {
        for (var i = 0; i < maxSlots; i++) slots.Add(new InventorySlot());
    }

    public Inventory(Inventory other)
    {
        for (var i = 0; i < other.GetMaxSlots(); i++) slots.Add(new InventorySlot(other.GetFilter(i), other.GetStack(i)));
    }

    public int GetMaxSlots() => slots.Count;

    public ItemCategory? GetFilter(int index) => slots[index].filter;

    public ItemStack GetStack(int index) => slots[index].stack;

    public bool SetStack(int index, ItemStack stack)
    {
        if (!slots[index].CanAccept(stack)) return false;
        slots[index].stack = stack;
        return true;
    }
    
    public int CountItems(ItemType type) => slots.Where(slot => slot.stack.itemType == type).Sum(slot => slot.stack.quantity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type">Item type to add</param>
    /// <param name="quantity">Amount to add</param>
    /// <returns>Quantity that was not able to be added</returns>
    public int AddItems(ItemType type, int quantity)
    {
        if (quantity <= 0) return 0;
        
        foreach (var slot in slots)
        {
            if (slot.stack.itemType == type)
            {
                var added = Mathf.Min(type.stackSize - slot.stack.quantity, quantity);
                quantity -= added;
                slot.stack = new ItemStack(type, slot.stack.quantity + added);
                if (quantity == 0) return 0;
            }
        }
        
        foreach (var slot in slots)
        {
            if (slot.stack.itemType == null)
            {
                var added = Mathf.Min(type.stackSize, quantity);
                quantity -= added;
                slot.stack = new ItemStack(type, added);
                if (quantity == 0) return 0;
            }
        }
        
        return quantity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type">Item type to remove</param>
    /// <param name="quantity">Amount to remove</param>
    /// <returns>Quantity that was successfully removed</returns>
    public int RemoveItems(ItemType type, int quantity)
    {
        if (quantity <= 0) return 0;
        
        var ret = 0;
        foreach (var slot in slots)
        {
            if (slot.stack.itemType == type)
            {
                var removed = Mathf.Min(slot.stack.quantity, quantity - ret);
                ret += removed;
                slot.stack = removed == slot.stack.quantity ? new ItemStack() : new ItemStack(type, slot.stack.quantity - removed);
                if (ret == quantity) return ret;
            }
        }
        return ret;
    }
}
