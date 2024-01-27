#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Inventory")]
public class InventoryVariable : ScriptableVariable<Inventory>
{
    [SerializeField] private Inventory inventory = null!;
    
    public override Inventory Provide() => inventory;

    public override void Consume(Inventory value) => inventory = value;
}
