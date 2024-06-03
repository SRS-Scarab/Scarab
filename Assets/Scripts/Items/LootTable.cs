#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    public GameObject? itemDropPrefab;
    public List<LootTableEntry> entries = new();
    
    public void OnLootDrop(Vector3 position)
    {
        if (itemDropPrefab == null) return;
        foreach (var entry in entries.Where(entry => entry.itemType != null))
        {
            var chance = Random.Range(0f, 1f);
            if (chance <= entry.dropChance)
            {
                var obj = Instantiate(itemDropPrefab, position, Quaternion.identity);
                var drop = obj.GetComponent<ItemDrop>();
                drop.Initialize(entry.itemType!, Random.Range(entry.minQuantity, entry.maxQuantity + 1));
                var rb = obj.GetComponent<Rigidbody>();
                rb.AddForce(Random.insideUnitSphere + Vector3.up * 2, ForceMode.Impulse);
            }
        }
    }

    [Serializable]
    public struct LootTableEntry
    {
        public ItemType? itemType;
        public int minQuantity;
        public int maxQuantity;
        public float dropChance;
    }
}
