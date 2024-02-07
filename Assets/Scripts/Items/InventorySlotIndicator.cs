#nullable enable
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotIndicator : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private Image? categoryIcon;
    [SerializeField] private Image? icon;
    [SerializeField] private Image? highlight;
    [SerializeField] private Button? button;
    [SerializeField] private TextMeshProUGUI? quantityText;
    
    [Header("State")]
    
    [SerializeField] private InventoryGrid? grid;
    [SerializeReference] private Inventory? target;
    [SerializeField] private int index;

    private void Update()
    {
        if (target != null)
        {
            var stack = target.GetStack(index);
            if (categoryIcon != null)
            {
                var filter = target.GetFilter(index);
                categoryIcon.enabled = stack.itemType == null && filter != null;
                if (filter != null) categoryIcon.sprite = filter.categoryIcon;
            }
            if (icon != null)
            {
                icon.enabled = stack.itemType != null;
                if (stack.itemType != null) icon.sprite = stack.itemType.icon;
            }
            if (highlight != null && grid != null) highlight.enabled = grid.IsSelected(index);
            if (quantityText != null) quantityText.text = stack.itemType == null || stack.itemType.stackSize == 1 ? "" : stack.quantity.ToString();
        }
    }

    public void Initialize(InventoryGrid owner, Inventory inventory, int i)
    {
        grid = owner;
        target = inventory;
        index = i;
        if (button != null) button.onClick.AddListener(OnSlotClicked);
    }
    
    private void OnSlotClicked()
    {
        if (grid != null) grid.OnIndexClicked(index);
    }
}
