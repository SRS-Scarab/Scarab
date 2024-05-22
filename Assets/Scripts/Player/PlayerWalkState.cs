#nullable enable
using UnityEngine;

public class PlayerWalkState : MonoState
{
    [SerializeField]
    private float walkSpeed;

    public override void OnTick(MonoStateMachine stateMachine, float delta)
    {
        base.OnTick(stateMachine, delta);
        
        var dependencyBlackboard = stateMachine.GetBlackboard<PlayerDependencyBlackboard>();
        var zoneBlackboard = stateMachine.GetBlackboard<PlayerZoneBlackboard>();
        if (dependencyBlackboard == null || !dependencyBlackboard.IsValid() || zoneBlackboard == null) return;
        
        var velocity = dependencyBlackboard.rigidbody!.velocity;
        var input = dependencyBlackboard.Actions!.Gameplay.Move.ReadValue<Vector2>().normalized;
        velocity.x = input.x * walkSpeed * zoneBlackboard.GetSpeedMultiplier();
        velocity.z = input.y * walkSpeed * zoneBlackboard.GetSpeedMultiplier();
        dependencyBlackboard.rigidbody!.velocity = velocity;
    }
}
