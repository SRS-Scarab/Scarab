#nullable enable
using UnityEngine;

public class EnemyFollowState : StateNode
{
    [SerializeField]
    private float followRange;
    
    [SerializeField]
    private float followSpeed;

    public bool IsApplicable()
    {
        var dependencies = GetBlackboard<EnemyDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return false;

        return (dependencies.playerEntityVar!.Provide()!.transform.position - transform.position).sqrMagnitude <= followRange * followRange;
    }

    protected override void OnTick(float delta)
    {
        base.OnTick(delta);
        
        var dependencies = GetBlackboard<EnemyDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;

        dependencies.agent!.speed = followSpeed;
        dependencies.agent!.SetDestination(dependencies.playerEntityVar!.Provide()!.transform.position);
    }
}
