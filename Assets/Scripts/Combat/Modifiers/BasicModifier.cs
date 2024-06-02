#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Basic")]
public class BasicModifier : CombatStatsModifier
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxMana;
    [SerializeField] private float manaRegen;
    [SerializeField] private float attack;
    [SerializeField] private float defence;
    
    public override CombatStats Modify(CombatStats stats)
    {
        stats.maxHealth += maxHealth;
        stats.maxMana += maxMana;
        stats.manaRegen += manaRegen;
        stats.attack += attack;
        stats.defence += defence;
        return stats;
    }
}
