#nullable enable
using UnityEngine;

public class EnemyAttackState : StateNode
{
    [SerializeField]
    private AttackPrefab? attack;
    
    [SerializeField]
    private float attackRange;
    
    [SerializeField]
    private float cooldown;
    
    [SerializeField]
    private float cooldownFinished;
    
    [SerializeField]
    private float delay;

    [SerializeField]
    private float delayFinished;
    
    public bool IsApplicable()
    {
        var dependencies = GetBlackboard<EnemyDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return false;

        return Time.time >= cooldownFinished && (dependencies.playerEntityVar!.Provide()!.transform.position - transform.position).sqrMagnitude <= attackRange * attackRange;
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        
        var dependencies = GetBlackboard<EnemyDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;

        var targetPos = dependencies.playerEntityVar!.Provide()!.transform.position;
        var offset = targetPos - transform.position;
        var angle = Vector2.SignedAngle(Vector2.right, new Vector2(offset.x, offset.z));
        if (attack != null && attack.TryInstantiate(dependencies.entity!, transform.position, angle))
        {
            cooldownFinished = Time.time + cooldown;
            delayFinished = Time.time + delay;
            SetBlocking(true);
        }
    }
    
    protected override void OnTick(float delta)
    {
        base.OnTick(delta);

        if (Time.time >= delayFinished)
        {
            SetBlocking(false);
        }
    }
}
