#nullable enable
using UnityEngine;

public class PlayerWalkState : MonoState
{
    [SerializeField]
    private float walkSpeed;

    public override void OnTick(MonoStateMachine stateMachine, float delta)
    {
        base.OnTick(stateMachine, delta);
        
        var blackboard = stateMachine.GetBlackboard<PlayerBlackboard>();
        if (blackboard == null || !blackboard.IsValid()) return;
        
        var velocity = blackboard.rigidbody!.velocity;
        var input = blackboard.Actions!.Gameplay.Move.ReadValue<Vector2>().normalized;
        velocity.x = input.x * walkSpeed;
        velocity.z = input.y * walkSpeed;
        blackboard.rigidbody!.velocity = velocity;
    }
}
