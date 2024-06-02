#nullable enable
public class EnemyIdleState : StateNode
{
    protected override void OnTick(float delta)
    {
        base.OnTick(delta);
        
        var dependencies = GetBlackboard<EnemyDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;
        
        dependencies.agent!.ResetPath();
    }
}
