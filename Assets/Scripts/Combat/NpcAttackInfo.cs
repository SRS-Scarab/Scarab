#nullable enable
using System;
using UnityEngine;

[Serializable]
public class NpcAttackInfo
{
    public AttackInfo attack;
    public float cooldown;
    public float range;
    public float cooldownRemaining;

    public void OnTick()
    {
        cooldownRemaining -= Time.deltaTime;
    }

    public bool CanAttack(CombatEntity source, CombatEntity target)
    {
        return cooldownRemaining <= 0 && (source.transform.position - target.transform.position).sqrMagnitude <= range * range;
    }
    
    public bool TryAttack(CombatEntity source, CombatEntity target)
    {
        if (!CanAttack(source, target)) return false;

        var sourcePos = source.transform.position;
        var rotation = Vector2.SignedAngle(Vector2.right, target.transform.position - sourcePos);
        if (attack.TryAttack(source, sourcePos, rotation))
        {
            cooldownRemaining = cooldown;
            return true;
        }

        return false;
    }
}