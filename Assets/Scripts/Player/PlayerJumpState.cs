#nullable enable
using UnityEngine;

public class PlayerJumpState : MonoState
{
    [SerializeField]
    private float jumpForce;

    public override void OnEnter(MonoStateMachine stateMachine)
    {
        base.OnEnter(stateMachine);
        
        var blackboard = stateMachine.GetBlackboard<PlayerBlackboard>();
        if (blackboard == null || !blackboard.IsValid()) return;

        blackboard.rigidbody!.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    protected override void OnEnterPropagate(MonoStateMachine stateMachine)
    {
        base.OnEnterPropagate(stateMachine);
        
        var blackboard = stateMachine.GetBlackboard<PlayerBlackboard>();
        if (blackboard == null || !blackboard.IsValid()) return;

        blackboard.rigidbody!.drag = 0;
    }
}