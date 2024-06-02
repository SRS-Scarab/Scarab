#nullable enable
using UnityEngine;

public abstract class AttackProcessor : MonoBehaviour
{
    [SerializeField]
    private AttackProcessor? next;

    public void Process(CombatEntity source, CombatEntity target)
    {
        if (ProcessImpl(source, target) && next != null)
        {
            next.Process(source, target);
        }
    }

    protected abstract bool ProcessImpl(CombatEntity source, CombatEntity target);
}
