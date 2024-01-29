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
                var stack = prevInventory.GetStack(prevIndex);
                if (stack.itemType == null)
                {
                    prevInventory = newInventory;
                    prevIndex = newIndex;
                }
                else
                {
                    prevInventory.SetStack(prevIndex, newInventory.GetStack(newIndex));
                    newInventory.SetStack(newIndex, stack);
                    prevInventory = null;
                    prevIndex = -1;
                }
            }
        }
    }
}
