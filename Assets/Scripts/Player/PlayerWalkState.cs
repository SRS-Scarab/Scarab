#nullable enable
using UnityEngine;

public class PlayerWalkState : StateNode
{
    [SerializeField]
    private float walkSpeed;

    protected override void OnTick(float delta)
    {
        base.OnTick(delta);
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        var values = GetBlackboard<PlayerValues>();
        if (dependencies == null || !dependencies.IsValid() || values == null) return;
        
        var velocity = dependencies.rigidbody!.velocity;
        var input = dependencies.Actions!.Gameplay.Move.ReadValue<Vector2>().normalized;
        velocity.x = input.x * walkSpeed * values.GetSpeedMultiplier();
        velocity.z = input.y * walkSpeed * values.GetSpeedMultiplier();
        dependencies.rigidbody!.velocity = velocity;
    }
}
