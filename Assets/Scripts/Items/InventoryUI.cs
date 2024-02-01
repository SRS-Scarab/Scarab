#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private InputSubsystem? inputSubsystem;
    [SerializeField] private ActionsVariable? actionsVar;
    [SerializeField] private CanvasGroup? group;
    [SerializeField] private InventoryVariable? equipmentVar;
    [SerializeField] private InventoryVariable? inventoryVar;
    [SerializeField] private InventoryVariable? hotbarVar;
    [SerializeField] private InventoryGrid? equipmentGrid;
    [SerializeField] private InventoryGrid? inventoryGrid;
    [SerializeField] private InventoryGrid? hotbarGrid;
    
    private void Start()
    {
        if (actionsVar != null) actionsVar.Provide().Gameplay.Inventory.performed += Activate;
        if (equipmentVar != null && equipmentGrid != null) equipmentGrid.Initialize(equipmentVar.Provide());
        if (inventoryVar != null && inventoryGrid != null) inventoryGrid.Initialize(inventoryVar.Provide());
        if (hotbarVar != null && hotbarGrid != null) hotbarGrid.Initialize(hotbarVar.Provide());
    }

    private void Activate(InputAction.CallbackContext context)
    {
        if (inputSubsystem != null) inputSubsystem.PushMap(nameof(Actions.UI));
        if (actionsVar != null)
        {
            actionsVar.Provide().Gameplay.Inventory.performed -= Activate;
            actionsVar.Provide().UI.Close.performed += Deactivate;
        }
        if (group != null)
        {
            group.alpha = 1;
            group.interactable = true;
            group.blocksRaycasts = true;
        }
    }

    private void Deactivate(InputAction.CallbackContext context)
    {
        if (inputSubsystem != null) inputSubsystem.PopMap();
        if (actionsVar != null)
        {
            actionsVar.Provide().Gameplay.Inventory.performed += Activate;
            actionsVar.Provide().UI.Close.performed -= Deactivate;
        }
        if (group != null)
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
    }
}
