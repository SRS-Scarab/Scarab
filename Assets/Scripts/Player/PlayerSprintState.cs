#nullable enable
using UnityEngine;

public class PlayerSprintState : StateNode
{
    [SerializeField]
    private float sprintSpeed;

    protected override void OnTick(float delta)
    {
        base.OnTick(delta);
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        var values = GetBlackboard<PlayerValues>();
        if (dependencies == null || !dependencies.IsValid() || values == null) return;
        
        var velocity = dependencies.rigidbody!.velocity;
        var input = dependencies.Actions!.Gameplay.Move.ReadValue<Vector2>().normalized;
        velocity.x = input.x * sprintSpeed * values.GetSpeedMultiplier();
        velocity.z = input.y * sprintSpeed * values.GetSpeedMultiplier();
        velocity = Quaternion.Euler(0, values.GetCameraDirection(), 0) * velocity;
        dependencies.rigidbody!.velocity = velocity;
    }
}
