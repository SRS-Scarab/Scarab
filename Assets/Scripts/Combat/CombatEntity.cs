#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private Rigidbody2D? rb;
    [SerializeField] private CombatEntityVariable? playerEntityVar;
    [SerializeField] private MoralitySubsystem? moralitySubsystem;
    
    [Header("Parameters")]
    
    [SerializeField] private CombatStats baseStats;
    [SerializeField] private List<CombatStatsModifier> modifiers = new();
    [SerializeField] private float iframeDuration;
    [SerializeField] private float moralAlignment;
    [SerializeField] private LootTable? lootTable;
    
    [Header("State")]
    
    [SerializeField] private float health;
    [SerializeField] private CombatStats stats;
    [SerializeField] private float iframeLeft;
    [SerializeField] private List<AttackInstance> processed = new();

    private void Start()
    {
        RecalculateStats();
        health = stats.maxHealth;
    }

    private void Update()
    {
        processed.RemoveAll(e => e == null);
        iframeLeft -= Time.deltaTime;
        RecalculateStats(); // todo remove this later
    }

    public float GetHealth() => health;

    public float GetMaxHealth() => stats.maxHealth;

    public float GetAttack() => stats.attack;

    public float GetDefence() => stats.defence;

    public void ProcessAttack(AttackInstance instance)
    {
        var source = instance.GetSource();
        if (!processed.Contains(instance) && iframeLeft <= 0 && source != null && source != this)
        {
            processed.Add(instance);
            iframeLeft = iframeDuration;
            health -= instance.GetAttackInfo().damage * source.GetAttack() / GetDefence();
            if (rb != null) rb.AddForce((transform.position - instance.transform.position).normalized * instance.GetAttackInfo().knockback, ForceMode2D.Impulse);
            if (health <= 0)
            {
                Destroy(gameObject);
                if (playerEntityVar != null && moralitySubsystem != null)
                {
                    if (source == playerEntityVar.Provide()) moralitySubsystem.ChangeMorality(-moralAlignment);
                }
                if (lootTable != null) lootTable.OnLootDrop(transform.position);
            }
        }
    }

    public void ProcessHeal(float hp, float mana)
    {
        health = Mathf.Clamp(health + hp, 0, GetMaxHealth());
        // todo add mana later
    }

    // todo call this everytime equipment changes instead of per frame
    private void RecalculateStats()
    {
        modifiers.Sort(new CombatStatsModifier.Comparer());
        stats = modifiers.Aggregate(baseStats, (cur, modifier) => modifier.Modify(cur));
    }
}
