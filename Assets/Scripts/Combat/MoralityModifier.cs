#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Morality")]
public class MoralityModifier : CombatStatsModifier
{
    [SerializeField] private MoralitySubsystem? subsystem;
    [SerializeField] private float maxBonusAttack;
    [SerializeField] private float maxBonusDefence;

    public override CombatStats Modify(CombatStats stats)
    {
        if (subsystem != null)
        {
            stats.attack += maxBonusAttack * Mathf.Clamp01(subsystem.GetMorality() / subsystem.GetBadThreshold());
            stats.defence += maxBonusDefence * Mathf.Clamp01(subsystem.GetMorality() / subsystem.GetGoodThreshold());
        }

        return stats;
    }
}
