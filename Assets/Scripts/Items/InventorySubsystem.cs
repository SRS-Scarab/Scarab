#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Subsystems/Inventory")]
public class InventorySubsystem : ScriptableObject
{
    [SerializeReference] private Inventory? prevInventory;
    [SerializeField] private int prevIndex = -1;

    public Inventory? GetSelectedInventory() => prevInventory;

    public int GetSelectedIndex() => prevIndex;
    
    public void OnSlotSelected(Inventory newInventory, int newIndex)
    {
        if (prevInventory == newInventory && prevIndex == newIndex)
        {
            prevInventory = null;
            prevIndex = -1;
        }
        else
        {
            if (prevInventory == null)
            {
                prevInventory = newInventory;
                prevIndex = newIndex;
            }
            else
            {
                var prevStack = prevInventory.GetStack(prevIndex);
                if (prevStack.itemType == null)
                {
                    prevInventory = newInventory;
                    prevIndex = newIndex;
                }
                else
                {
                    var newStack = newInventory.GetStack(newIndex);
                    if (prevInventory.SetStack(prevIndex, newStack))
                    {
                        if (newInventory.SetStack(newIndex, prevStack))
                        {
                            prevInventory = null;
                            prevIndex = -1;
                        }
                        else
                        {
                            prevInventory.SetStack(prevIndex, prevStack);
                        }
                    }
                }
            }
        }
    }
}
