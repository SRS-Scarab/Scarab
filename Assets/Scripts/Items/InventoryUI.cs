#nullable enable
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private CanvasGroup? group;
    [SerializeField] private InventoryVariable? equipmentVar;
    [SerializeField] private InventoryVariable? inventoryVar;
    [SerializeField] private InventoryVariable? hotbarVar;
    [SerializeField] private InventoryGrid? equipmentGrid;
    [SerializeField] private InventoryGrid? inventoryGrid;
    [SerializeField] private InventoryGrid? hotbarGrid;

    private void Start()
    {
        if (equipmentVar != null && equipmentGrid != null) equipmentGrid.Initialize(equipmentVar.Provide());
        if (inventoryVar != null && inventoryGrid != null) inventoryGrid.Initialize(inventoryVar.Provide());
        if (hotbarVar != null && hotbarGrid != null) hotbarGrid.Initialize(hotbarVar.Provide());
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) Toggle();
    }

    public void Toggle()
    {
        if (group != null)
        {
            if(group.alpha == 0) Activate();
            else Deactivate();
        }
    }

    public void Activate()
    {
        if (group != null)
        {
            group.alpha = 1;
            group.interactable = true;
            group.blocksRaycasts = true;
        }
    }
    
    public void Deactivate()
    {
        if (group != null)
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
    }
}
