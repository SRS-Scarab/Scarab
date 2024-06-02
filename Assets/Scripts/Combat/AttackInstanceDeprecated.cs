#nullable enable
using UnityEngine;

public class AttackInstanceDeprecated : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private SpriteRenderer? sprite;
    
    [Header("State")]
    
    [SerializeField] private CombatEntity? source;
    [SerializeField] private AttackInfo info;
    [SerializeField] private float time;

    public CombatEntity? GetSource() => source;

    public AttackInfo GetAttackInfo() => info;

    public void Initialize(CombatEntity entity, AttackInfo attack)
    {
        source = entity;
        info = attack;
        Destroy(gameObject, attack.persist);
    }

    private void Update()
    {
        if (source == null) Destroy(gameObject);
        else
        {
            time += Time.deltaTime;
            if (sprite != null)
            {
                var color = sprite.color;
                color.a = 1 - time / info.persist;
                sprite.color = color;
            }
        }
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
