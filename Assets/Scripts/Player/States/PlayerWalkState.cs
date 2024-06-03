#nullable enable
using UnityEngine;

public class PlayerWalkState : StateNode
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private AudioClip walkClip;

    protected override void OnTick(float delta)
    {
        base.OnTick(delta);
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        var values = GetBlackboard<PlayerValues>();
        if (dependencies == null || !dependencies.IsValid() || values == null) return;
        
        var input = dependencies.Actions!.Gameplay.Move.ReadValue<Vector2>();
        if (input.magnitude == 0)
        {
            values.moveVelocity = Vector3.zero;
        }
        else
        {
            input = input.normalized;
            var velocity = new Vector3(input.x, 0, input.y) * (walkSpeed * values.GetSpeedMultiplier());
            velocity = Quaternion.Euler(0, values.GetCameraAngle(), 0) * velocity;
            dependencies.rigidbody!.position += velocity * delta;
            SoundFXManager.instance.PlaySound(walkClip, transform.position, 0.5f);

            values.moveVelocity = velocity;
        }
    }
}
