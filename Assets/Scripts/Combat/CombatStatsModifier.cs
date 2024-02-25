#nullable enable
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatStatsModifier : ScriptableObject
{
    [SerializeField] private int priority;

    public int GetPriority() => priority;
    
    public abstract CombatStats Modify(CombatStats stats);

    public class Comparer : IComparer<CombatStatsModifier>
    {
        public int Compare(CombatStatsModifier x, CombatStatsModifier y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            return -x.priority.CompareTo(y.priority);
        }
    }
}
