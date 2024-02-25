#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Multiply")]
public class MultiplyModifier : CombatStatsModifier
{
    [SerializeField] private float maxHealthMultiplier = 1;
    [SerializeField] private float attackMultiplier = 1;
    [SerializeField] private float defenceMultiplier = 1;
    
    public override CombatStats Modify(CombatStats stats)
    {
        stats.maxHealth *= maxHealthMultiplier;
        stats.attack *= attackMultiplier;
        stats.defence *= defenceMultiplier;
        return stats;
    }
}
