#nullable enable
using UnityEngine;

public class PlayerDashState : MonoState
{
    [SerializeField]
    private float dashForce;

    [SerializeField]
    private float threshold;

    public override void OnEnter(MonoStateMachine stateMachine)
    {
        base.OnEnter(stateMachine);
        
        var blackboard = stateMachine.GetBlackboard<PlayerDependencyBlackboard>();
        if (blackboard == null || !blackboard.IsValid()) return;

        var input = blackboard.Actions!.Gameplay.Move.ReadValue<Vector2>().normalized;

        blackboard.rigidbody!.useGravity = false;
        blackboard.rigidbody.drag = 3;
        blackboard.rigidbody.AddForce(new Vector3(input.x * dashForce, 0, input.y * dashForce), ForceMode.Impulse);
    }

    protected override void OnTickPropagate(MonoStateMachine stateMachine, float delta)
    {
        var blackboard = stateMachine.GetBlackboard<PlayerDependencyBlackboard>();
        if (blackboard == null || !blackboard.IsValid()) return;
        
        var groundSpeed = blackboard.rigidbody!.velocity;
        groundSpeed.y = 0;
        if (groundSpeed.magnitude < threshold)
        {
            base.OnTickPropagate(stateMachine, delta);
        }
    }

    public override void OnExit(MonoStateMachine stateMachine)
    {
        base.OnExit(stateMachine);
        
        var blackboard = stateMachine.GetBlackboard<PlayerDependencyBlackboard>();
        if (blackboard == null || !blackboard.IsValid()) return;

        blackboard.rigidbody!.useGravity = true;
    }
}