#nullable enable
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CombatEntity : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private Rigidbody2D? rb;
    [SerializeField] private InventoryVariable? equipmentSource;
    [SerializeField] private CombatEntityVariable? playerEntityVar;
    [SerializeField] private MoralitySubsystem? moralitySubsystem;
    
    [Header("Parameters")]
    
    [SerializeField] private Inventory equipment = new();
    [SerializeField] private float baseMaxHealth;
    [SerializeField] private float baseAttack;
    [SerializeField] private float baseDefence;
    [SerializeField] private float iframeDuration;
    [SerializeField] private float moralAlignment;
    [SerializeField] private bool useMoralityBonus;
    
    [Header("State")]
    
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float attack;
    [SerializeField] private float defence;
    [SerializeField] private float iframeLeft;
    [SerializeField] private List<AttackInstance> processed = new();

    private void Start()
    {
        RecalculateStats();
        health = maxHealth;
    }

    private void Update()
    {
        processed.RemoveAll(e => e == null);
        iframeLeft -= Time.deltaTime;
        RecalculateStats(); // todo remove this later
    }

    public float GetHealth() => health;

    public float GetMaxHealth() => maxHealth;

    public float GetAttack() => attack;

    public float GetDefence() => defence;

    public void ProcessAttack(AttackInstance instance)
    {
        var source = instance.GetSource();
        if (!processed.Contains(instance) && iframeLeft <= 0 && source != null && source != this)
        {
            processed.Add(instance);
            iframeLeft = iframeDuration;
            health -= instance.GetAttackInfo().damage * source.attack / defence;
            if (rb != null) rb.AddForce((transform.position - instance.transform.position).normalized * instance.GetAttackInfo().knockback, ForceMode2D.Impulse);
            if (health <= 0)
            {
                Destroy(gameObject);
                if (playerEntityVar != null && moralitySubsystem != null)
                {
                    if (source == playerEntityVar.Provide())
                    {
                        // you killed someone!
                        moralitySubsystem.ChangeMorality(-moralAlignment);
                    }
                }
            }
        }
    }

    public void ProcessHeal(float hp, float mana)
    {
        health = Mathf.Clamp(health + hp, 0, maxHealth);
        // todo add mana later
    }

    // todo call this everytime equipment changes instead of per frame
    public void RecalculateStats()
    {
        maxHealth = baseMaxHealth;
        attack = baseAttack;
        defence = baseDefence;
        
        var target = equipmentSource == null ? equipment : equipmentSource.Provide();
        for (var i = 0; i < target.GetMaxSlots(); i++)
        {
            var stack = target.GetStack(i);
            if (stack.itemType != null && stack.itemType is EquipmentType type)
            {
                maxHealth += type.maxHealth;
                attack += type.attack;
                defence += type.defence;
            }
        }

        if (useMoralityBonus && moralitySubsystem != null)
        {
            attack += moralitySubsystem.GetBonusAttack();
            defence += moralitySubsystem.GetBonusDefence();
        }

        maxHealth = Mathf.Max(maxHealth, 0);
        attack = Mathf.Max(attack, 0);
        defence = Mathf.Max(defence, 0);
    }
}
