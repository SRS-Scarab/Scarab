#nullable enable
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Subsystems/Inventory")]
public class InventorySubsystem : ScriptableObject
{
    [SerializeField] private OnSelectionBehavior behavior;
    
    [NonSerialized] private Inventory? _curInventory;
    [NonSerialized] private int _curIndex = -1;
    
    public Inventory? GetSelectedInventory() => _curInventory;

    public int GetSelectedIndex() => _curIndex;

    public void OnSlotSelected(Inventory newInventory, int newIndex)
    {
        if (behavior == OnSelectionBehavior.Select) Select(newInventory, newIndex);
        else Swap(newInventory, newIndex);
    }

    private void Select(Inventory newInventory, int newIndex)
    {
        _curInventory = newInventory;
        _curIndex = newIndex;
    }
    
    private void Swap(Inventory newInventory, int newIndex)
    {
        if (_curInventory == newInventory && _curIndex == newIndex)
        {
            _curInventory = null;
            _curIndex = -1;
        }
        else
        {
            if (_curInventory == null)
            {
                _curInventory = newInventory;
                _curIndex = newIndex;
            }
            else
            {
                var prevStack = _curInventory.GetStack(_curIndex);
                if (prevStack.itemType == null)
                {
                    _curInventory = newInventory;
                    _curIndex = newIndex;
                }
                else
                {
                    var newStack = newInventory.GetStack(newIndex);
                    if (prevStack.itemType == newStack.itemType)
                    {
                        var added = Mathf.Min(newStack.itemType.stackSize - newStack.quantity, prevStack.quantity);
                        newStack.quantity += added;
                        newInventory.SetStack(newIndex, newStack);
                        prevStack.quantity -= added;
                        _curInventory.SetStack(_curIndex, prevStack.quantity == 0 ? new ItemStack() : prevStack);
                        _curInventory = null;
                        _curIndex = -1;
                    }
                    else
                    {
                        if (_curInventory.SetStack(_curIndex, newStack))
                        {
                            if (newInventory.SetStack(newIndex, prevStack))
                            {
                                _curInventory = null;
                                _curIndex = -1;
                            }
                            else
                            {
                                _curInventory.SetStack(_curIndex, prevStack);
                            }
                        }
                    }
                }
            }
        }
    }

    private enum OnSelectionBehavior
    {
        Select,
        Swap
    }
}
