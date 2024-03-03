#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Subsystems/Upgrades")]
public class UpgradesSubsystem : ScriptableObject
{
    [SerializeField] private UpgradeEntry[] availableUpgrades = Array.Empty<UpgradeEntry>();
    [NonSerialized] private readonly List<UpgradeEntry> _acquiredUpgrades = new();

    public IEnumerable<UpgradeEntry> GetUpgrades() => availableUpgrades;

    public IEnumerable<UpgradeEntry> GetAcquiredUpgrades() => _acquiredUpgrades;

    public void AcquireUpgrade(UpgradeEntry entry) => _acquiredUpgrades.Add(entry);
}

[Serializable]
public class UpgradeEntry
{
    public string name = string.Empty;
    public Sprite? icon;
    public string description = string.Empty;
    public int cost;
    public CombatStatsModifier modifier = null!;
}
