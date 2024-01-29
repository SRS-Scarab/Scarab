#nullable enable
using System;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] public Interactable? interactable;
    
    [Header("State")]
    
    [SerializeField] private ItemType? itemType;
    [SerializeField] private int quantity;

    private void Update()
    {
        if(itemType == null) Destroy(gameObject);
    }

    public void Initialize(ItemType item, int amount)
    {
        itemType = item;
        quantity = amount;
        GetComponent<SpriteRenderer>().sprite = itemType.icon;
        if (interactable != null)
        {
            interactable.SetPromptText($"{itemType.name} ({quantity})");
            interactable.OnInteract += OnPickUp;
        }
    }

    private void OnPickUp(object sender, EventArgs args)
    {
        if (interactable != null) interactable.OnInteract -= OnPickUp;
        // todo add to player inventory
    }
}
