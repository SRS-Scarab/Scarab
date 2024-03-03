#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Upgrades")]
public class UpgradesModifier : AggregateModifier
{
    [SerializeField] private UpgradesSubsystem? subsystem;

    public override CombatStats Modify(CombatStats stats)
    {
        Clear();
        
        if (subsystem != null)
        {
            foreach (var upgrade in subsystem.GetAcquiredUpgrades())
            {
                Add(upgrade.modifier);
            }
        }

        return base.Modify(stats);
    }
}
