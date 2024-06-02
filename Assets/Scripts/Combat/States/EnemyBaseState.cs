#nullable enable
using UnityEngine;

public class EnemyBaseState : StateNode
{
    [SerializeField]
    private EnemyIdleState? idleState;
    
    [SerializeField]
    private EnemyFollowState? followState;
    
    [SerializeField]
    private EnemyAttackState? attackState;
    
    private void Update()
    {
        OnTick(Time.deltaTime);
    }

    protected override void OnTick(float delta)
    {
        base.OnTick(delta);
        
        var dependencies = GetBlackboard<EnemyDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;
        
        // Blocking states automatically handled by SetCurrent
        
        if (attackState != null && attackState.IsApplicable())
        {
            SetCurrent(attackState);
            return;
        }

        if (followState != null && followState.IsApplicable())
        {
            SetCurrent(followState);
            return;
        }
        
        SetCurrent(idleState);
    }
}
