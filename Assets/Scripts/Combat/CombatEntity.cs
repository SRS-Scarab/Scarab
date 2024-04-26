#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;

public class CombatEntity : MonoBehaviour, IFragmentSaveable
{
    private const string ID = "combat-entity";

    [Header("Dependencies")]

    [SerializeField] private Rigidbody2D? rb;
    [SerializeField] private CombatEntityVariable? playerEntityVar;
    [SerializeField] private MoralitySubsystem? moralitySubsystem;
    [SerializeField] private CameraVariable? mainCamera;

    [Header("Parameters")]

    [SerializeField] private CombatStats baseStats;
    [SerializeField] private List<CombatStatsModifier> modifiers = new();
    [SerializeField] private float iframeDuration;
    [SerializeField] private float moralAlignment;
    [SerializeField] private LootTable? lootTable;

    [Header("State")]

    [SerializeField] private float health;
    [SerializeField] private float mana;
    [SerializeField] private CombatStats stats;
    [SerializeField] private float iframeLeft;
    [SerializeField] private List<AttackInstance> processed = new();

    private void Start()
    {
        RecalculateStats();
        health = stats.maxHealth;
        mana = stats.maxMana;
    }

    private void Update()
    {
        processed.RemoveAll(e => e == null);
        iframeLeft -= Time.deltaTime;
        RecalculateStats(); // todo remove this later
        mana = Mathf.Clamp(mana + stats.manaRegen * Time.deltaTime, 0, stats.maxMana);
    }

    public float GetHealth() => health;

    public float GetMaxHealth() => stats.maxHealth;

    public float GetMana() => mana;

    public float GetMaxMana() => stats.maxMana;

    public bool DeductMana(float amount)
    {
        if (mana < amount) return false;
        mana -= amount;
        return true;
    }

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
            if (playerEntityVar != null && this == playerEntityVar.Provide()) DamageFlash.Flash(0.7f, 0.1f, 0.4f, Color.red);
            if (rb != null) rb.AddForce((transform.position - instance.transform.position).normalized * instance.GetAttackInfo().knockback, ForceMode2D.Impulse);
            if (health <= 0)
            {
                // if player dies, respawn them
                if (playerEntityVar != null && this == playerEntityVar.Provide())
                {
                    // TODO: get respawn position from save data
                    DamageFlash.StopFlash();
                    GameObject newObj = Instantiate(gameObject, new Vector3(0, 0, 0), Quaternion.identity);
                    if (mainCamera != null) mainCamera.Provide().transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().Follow = newObj.transform;
                }
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

    public string GetId()
    {
        return ID;
    }

    public SaveFragmentBase Save()
    {
        return new CombatEntityFragmentV0(health, mana, GetId());
    }

    public void Load(SaveFragmentBase fragment)
    {
        var latest = (CombatEntityFragmentV0)fragment.GetLatest();
        health = latest.Health;
        mana = latest.Mana;
    }
}
