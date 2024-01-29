#nullable enable
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private Image? icon;
    [SerializeField] private Image? highlight;
    [SerializeField] private Button? button;
    [SerializeField] private TextMeshProUGUI? quantityText;
    
    [Header("State")]
    
    [SerializeField] private InventoryGrid? grid;
    [SerializeField] private Inventory? target;
    [SerializeField] private int index;

    private void Update()
    {
        if (target != null)
        {
            var stack = target.GetStack(index);
            if (icon != null)
            {
                if (stack.itemType == null) icon.color = Color.clear;
                else
                {
                    icon.color = Color.white;
                    icon.sprite = stack.itemType.icon;
                }
            }
            if (highlight != null && grid != null) highlight.enabled = grid.IsSelected(index);
            if (quantityText != null) quantityText.text = stack.quantity == 0 ? "" : stack.quantity.ToString();
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
