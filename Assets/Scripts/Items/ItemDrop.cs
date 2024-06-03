#nullable enable
using System;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [Header("Dependencies")]

    [SerializeField] public Interactable? interactable;
    [SerializeField] public InventoryVariable? hotbarVar;
    [SerializeField] public InventoryVariable? inventoryVar;

    [Header("State")]

    [SerializeField] private ItemType? itemType;
    [SerializeField] private int quantity;

    private void Update()
    {
        if (itemType == null || quantity == 0)
        {
            if (interactable != null) interactable.OnInteract -= OnPickUp;
            Destroy(gameObject);
        }
    }

    public void Initialize(ItemType item, int amount)
    {
        itemType = item;
        quantity = amount;
        if (interactable != null)
        {
            interactable.SetPromptText($"{itemType.itemName} ({quantity})");
            interactable.OnInteract += OnPickUp;
        }
    }

    private void OnPickUp(object sender, EventArgs args)
    {
        if (quantity > 0 && itemType?.itemName == "Gold Coin") { CoinManager.AddCoins(quantity); quantity = 0; }
        if (quantity > 0 && hotbarVar != null) quantity = hotbarVar.Provide().AddItems(itemType!, quantity);
        if (quantity > 0 && inventoryVar != null) quantity = inventoryVar.Provide().AddItems(itemType!, quantity);
        if (quantity == 0)
        {
            if (interactable != null) interactable.OnInteract -= OnPickUp;
            Destroy(gameObject);
        }
    }
}
