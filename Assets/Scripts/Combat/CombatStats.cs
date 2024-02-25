#nullable enable
using System;

[Serializable]
public struct CombatStats
{
    public float maxHealth;
    public float attack;
    public float defence;

    public CombatStats(float maxHealth, float attack, float defence)
    {
        this.maxHealth = maxHealth;
        this.attack = attack;
        this.defence = defence;
    }
}
