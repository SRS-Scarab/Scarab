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
    
    public void Attack(CombatEntity source, CombatEntity target)
    {
        if (!CanAttack(source, target)) return;

        var sourcePos = source.transform.position;
        var rotation = Vector2.SignedAngle(Vector2.right, target.transform.position - sourcePos);
        attack.Attack(source, sourcePos, rotation);
        cooldownRemaining = cooldown;
    }
}