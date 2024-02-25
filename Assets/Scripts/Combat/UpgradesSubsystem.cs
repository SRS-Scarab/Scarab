#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Subsystems/Upgrades")]
public class UpgradesSubsystem : ScriptableObject
{
    [SerializeField] private UpgradeEntry[] availableUpgrades = Array.Empty<UpgradeEntry>();
    [NonSerialized] private readonly List<UpgradeEntry> _acquiredUpgrades = new();

    public IEnumerable<UpgradeEntry> GetAcquiredUpgrades() => _acquiredUpgrades.AsEnumerable();
}

[Serializable]
public struct UpgradeEntry
{
    public string name;
    public int cost;
    public CombatStatsModifier modifier;
}
