#nullable enable
using UnityEngine;

public class AttackInstance : MonoBehaviour
{
    [Header("State")]
    
    [SerializeField] private CombatEntity? source;
    [SerializeField] private AttackInfo info;

    public CombatEntity? GetSource() => source;

    public AttackInfo GetAttackInfo() => info;

    public void Initialize(CombatEntity entity, AttackInfo attack)
    {
        source = entity;
        info = attack;
        Destroy(gameObject, attack.persist);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        var entity = other.GetComponent<CombatEntity>();
        if (entity != null)
        {
            entity.ProcessAttack(this);
        }
    }
}
