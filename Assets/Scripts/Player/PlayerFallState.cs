#nullable enable
public class PlayerFallState : StateNode
{
    protected override void OnEnter()
    {
        base.OnEnter();
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;

        dependencies.rigidbody!.drag = 0;
    }
}