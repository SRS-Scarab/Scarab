#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Aggregate")]
public class AggregateModifier : CombatStatsModifier
{
    [NonSerialized] private readonly List<CombatStatsModifier> _modifiers = new();

    public void Clear() => _modifiers.Clear();

    public void Add(CombatStatsModifier modifier) => _modifiers.Add(modifier);

    public void Remove(CombatStatsModifier modifier) => _modifiers.Remove(modifier);
    
    public override CombatStats Modify(CombatStats stats)
    {
        _modifiers.Sort(new Comparer());
        return _modifiers.Aggregate(stats, (cur, modifier) => modifier.Modify(cur));
    }
}
