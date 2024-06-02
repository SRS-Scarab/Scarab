#nullable enable
using System;

[Serializable]
public struct CombatStats
{
    public float maxHealth;
    public float maxMana;
    public float manaRegen;
    public float attack;
    public float defence;

    public CombatStats(float maxHealth, float maxMana, float manaRegen, float attack, float defence)
    {
        this.maxHealth = maxHealth;
        this.maxMana = maxMana;
        this.manaRegen = manaRegen;
        this.attack = attack;
        this.defence = defence;
    }
}
