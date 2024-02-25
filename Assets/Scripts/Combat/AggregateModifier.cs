#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Aggregate")]
public class AggregateModifier : CombatStatsModifier
{
    [SerializeField] private List<CombatStatsModifier> modifiers = new();

    public void Clear() => modifiers.Clear();

    public void Add(CombatStatsModifier modifier) => modifiers.Add(modifier);

    public void Remove(CombatStatsModifier modifier) => modifiers.Remove(modifier);
    
    public override CombatStats Modify(CombatStats stats)
    {
        modifiers.Sort(new Comparer());
        return modifiers.Aggregate(stats, (cur, modifier) => modifier.Modify(cur));
    }
}
