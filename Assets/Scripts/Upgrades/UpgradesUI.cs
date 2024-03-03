#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradesUI : MonoBehaviour
{
    [SerializeField]
    private InputSubsystem inputSubsystem = null!;
    
    [SerializeField]
    private ActionsVariable actionsVar = null!;
    
    [SerializeField]
    private CanvasGroup group = null!;
    
    [SerializeField]
    private Transform content = null!;
    
    [SerializeField]
    private GameObject entryPrefab = null!;
    
    [SerializeField]
    private UpgradesSubsystem upgradesSubsystem = null!;
    
    private void Start()
    {
        actionsVar.Provide().Gameplay.Upgrades.performed += Activate;
        
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
    
    private void Activate(InputAction.CallbackContext context)
    {
        inputSubsystem.PushMap(nameof(Actions.UI));
        
        actionsVar.Provide().Gameplay.Upgrades.performed -= Activate;
        actionsVar.Provide().UI.CloseUpgrades.performed += Deactivate; // only register here as pressing escape in other ui screens can cause unintended mapping changes
        
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;

        while (content.childCount > 0)
        {
            var obj = content.GetChild(0);
            obj.SetParent(null);
            Destroy(obj.gameObject);
        }

        foreach (var entry in upgradesSubsystem.GetUpgrades())
        {
            var obj = Instantiate(entryPrefab, content);
            obj.GetComponent<UpgradeEntryUI>().Initialize(entry);
        }
    }

    private void Deactivate(InputAction.CallbackContext context)
    {
        inputSubsystem.PopMap();
        
        actionsVar.Provide().Gameplay.Upgrades.performed += Activate;
        actionsVar.Provide().UI.CloseUpgrades.performed -= Deactivate;
        
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }
}
