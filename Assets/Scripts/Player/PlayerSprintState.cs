#nullable enable
using UnityEngine;

public class PlayerSprintState : MonoState
{
    [SerializeField]
    private float sprintSpeed;

    public override void OnTick(MonoStateMachine stateMachine, float delta)
    {
        base.OnTick(stateMachine, delta);
        
        var blackboard = stateMachine.GetBlackboard<PlayerBlackboard>();
        if (blackboard == null || !blackboard.IsValid()) return;
        
        var velocity = blackboard.rigidbody!.velocity;
        var input = blackboard.Actions!.Gameplay.Move.ReadValue<Vector2>().normalized;
        velocity.x = input.x * sprintSpeed;
        velocity.z = input.y * sprintSpeed;
        blackboard.rigidbody!.velocity = velocity;
    }
}
