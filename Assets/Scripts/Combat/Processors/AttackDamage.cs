#nullable enable
using UnityEngine;

public class AttackDamage : AttackProcessor
{
    [SerializeField]
    private float damage;
    
    protected override bool ProcessImpl(CombatEntity source, CombatEntity target)
    {
        return target.ProcessDamage(source, damage);
    }
}
