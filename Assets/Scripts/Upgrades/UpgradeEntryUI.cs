#nullable enable
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeEntryUI : MonoBehaviour
{
    [SerializeField]
    private Image iconImage = null!;

    [SerializeField]
    private TextMeshProUGUI nameText = null!;

    [SerializeField]
    private TextMeshProUGUI descriptionText = null!;

    [SerializeField]
    private Button buyButton = null!;

    [SerializeField]
    private TextMeshProUGUI costText = null!;

    [SerializeField]
    private UpgradesSubsystem upgradesSubsystem = null!;

    [SerializeField]
    private InventoryVariable inventoryVar = null!;

    [SerializeField]
    private InventoryVariable hotbarVar = null!;

    [SerializeReference]
    private UpgradeEntry? entry;

    public void Initialize(UpgradeEntry inEntry)
    {
        entry = inEntry;

        iconImage.sprite = entry.icon;
        nameText.text = entry.name;
        descriptionText.text = entry.description;
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        costText.text = upgradesSubsystem.GetAcquiredUpgrades().Contains(entry) ? "Acquired" : $"{entry.cost} Gold";
    }

    private void Update()
    {
        if (entry != null)
        {
            if (upgradesSubsystem.GetAcquiredUpgrades().Contains(entry))
            {
                buyButton.interactable = false;
                return;
            }
            var total = CoinManager.GetCoins();
            buyButton.interactable = total >= entry.cost;
        }
    }

    private void OnBuyButtonClicked()
    {
        if (entry != null)
        {
            var total = CoinManager.GetCoins();
            if (total >= entry.cost)
            {
                CoinManager.AddCoins(-entry.cost);
                upgradesSubsystem.AcquireUpgrade(entry);
            }
        }
    }
}
