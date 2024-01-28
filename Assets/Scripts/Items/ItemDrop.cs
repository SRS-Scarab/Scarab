#nullable enable
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public ItemType? itemType;
    public int quantity;

    private void Start()
    {
        if (itemType == null) return;
        GetComponent<SpriteRenderer>().sprite = itemType.icon;
    }

    private void Update()
    {
        if(itemType == null) Destroy(gameObject);
    }
}
