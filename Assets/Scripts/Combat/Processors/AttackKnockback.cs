#nullable enable
using UnityEngine;

public class AttackKnockback : AttackProcessor
{
    [SerializeField]
    private float knockback;

    protected override bool ProcessImpl(CombatEntity source, CombatEntity target)
    {
        target.ProcessKnockback(transform.position, knockback);
        return true;
    }
}
