#nullable enable
public class PlayerFallState : MonoState
{
    protected override void OnEnterPropagate(MonoStateMachine stateMachine)
    {
        base.OnEnterPropagate(stateMachine);
        
        var blackboard = stateMachine.GetBlackboard<PlayerDependencyBlackboard>();
        if (blackboard == null || !blackboard.IsValid()) return;

        blackboard.rigidbody!.drag = 0;
    }
}