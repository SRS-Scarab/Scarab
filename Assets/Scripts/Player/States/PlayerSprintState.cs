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
        
        var input = dependencies.Actions!.Gameplay.Move.ReadValue<Vector2>();
        if (input.magnitude > 0)
        {
            input = input.normalized;
            var deltaPos = new Vector3(input.x, 0, input.y) * (sprintSpeed * values.GetSpeedMultiplier() * delta);
            deltaPos = Quaternion.Euler(0, values.GetCameraAngle(), 0) * deltaPos;
            dependencies.rigidbody!.position += deltaPos;
        }
    }
}
