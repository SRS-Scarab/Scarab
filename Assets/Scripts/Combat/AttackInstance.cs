#nullable enable
using System;
using UnityEngine;

public class AttackInstance : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private SpriteRenderer? sprite;
    [SerializeField] private Transform? maskTransform;
    
    [Header("Parameters")]
    
    [SerializeField] private float startAngle;
    [SerializeField] private float endAngle;
    
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
            if (maskTransform != null)
            {
                var angle = maskTransform.localEulerAngles;
                angle.z = time >= info.persist / 2 ? endAngle : time / (info.persist / 2) * (endAngle - startAngle) + startAngle;
                maskTransform.localEulerAngles = angle;
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
