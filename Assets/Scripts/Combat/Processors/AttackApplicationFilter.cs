#nullable enable
using System.Collections.Generic;
using UnityEngine;

public class AttackApplicationFilter : AttackProcessor
{
    [SerializeField]
    private List<CombatEntity> applied = new();

    protected override bool ProcessImpl(CombatEntity source, CombatEntity target)
    {
        if (source == target) return false;
        if (applied.Contains(target)) return false;
        applied.Add(target);
        return true;
    }
}
