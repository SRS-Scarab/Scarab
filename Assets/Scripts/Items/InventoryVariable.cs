#nullable enable
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Inventory")]
public class InventoryVariable : ScriptableVariable<Inventory>
{
    [SerializeField] private Inventory inventory = new();
    [NonSerialized] private Inventory? _runtimeInventory;

    public override Inventory Provide() => _runtimeInventory ??= new Inventory(inventory);

    public override void Consume(Inventory value) => _runtimeInventory = value;
}
