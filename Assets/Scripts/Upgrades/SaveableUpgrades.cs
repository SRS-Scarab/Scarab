#nullable enable
using System.Linq;
using UnityEngine;

public class SaveableUpgrades : MonoBehaviour, IFragmentSaveable
{
    private const string ID = "upgrades";
    
    [SerializeField]
    private UpgradesSubsystem subsystem = null!;

    public string GetId()
    {
        return ID;
    }

    public SaveFragmentBase Save()
    {
        return new UpgradesFragmentV0(subsystem.GetAcquiredUpgrades().Select(e => e.name).ToList(), GetId());
    }

    public void Load(SaveFragmentBase fragment)
    {
        var latest = (UpgradesFragmentV0)fragment.GetLatest();
        latest.Upgrades.ForEach(upgradeName =>
        {
            subsystem.AcquireUpgrade(subsystem.GetUpgrades().First(entry => entry.name == upgradeName));
        });
    }
}
