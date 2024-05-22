#nullable enable
using UnityEngine;

public class PlayerSprintState : MonoState
{
    [SerializeField]
    private float sprintSpeed;

    public override void OnTick(MonoStateMachine stateMachine, float delta)
    {
        base.OnTick(stateMachine, delta);
        
        var dependencyBlackboard = stateMachine.GetBlackboard<PlayerDependencyBlackboard>();
        var zoneBlackboard = stateMachine.GetBlackboard<PlayerZoneBlackboard>();
        if (dependencyBlackboard == null || !dependencyBlackboard.IsValid() || zoneBlackboard == null) return;
        
        var velocity = dependencyBlackboard.rigidbody!.velocity;
        var input = dependencyBlackboard.Actions!.Gameplay.Move.ReadValue<Vector2>().normalized;
        velocity.x = input.x * sprintSpeed * zoneBlackboard.GetSpeedMultiplier();
        velocity.z = input.y * sprintSpeed * zoneBlackboard.GetSpeedMultiplier();
        dependencyBlackboard.rigidbody!.velocity = velocity;
    }
}
