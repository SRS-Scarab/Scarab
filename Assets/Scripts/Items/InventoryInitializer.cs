using UnityEngine;

public class InventoryInitializer : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private InventoryVariable[] inventories;

    private void Awake()
    {
        foreach (var inventory in inventories) inventory.Initialize();
    }
}
