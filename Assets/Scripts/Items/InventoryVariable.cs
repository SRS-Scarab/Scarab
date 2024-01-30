#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Inventory")]
public class InventoryVariable : ScriptableVariable<Inventory>
{
    [SerializeField] private Inventory inventory = null!;
    [SerializeReference] private Inventory? runtimeInventory;

    public void Initialize() => runtimeInventory = new Inventory(inventory);
    
    public override Inventory Provide() => runtimeInventory ??= new Inventory(inventory);

    public override void Consume(Inventory value) => runtimeInventory = value;
}
