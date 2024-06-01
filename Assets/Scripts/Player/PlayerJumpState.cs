#nullable enable
using UnityEngine;

public class PlayerJumpState : StateNode
{
    [SerializeField]
    private float jumpForce;

    protected override void OnEnter()
    {
        base.OnEnter();
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        var values = GetBlackboard<PlayerValues>();
        if (dependencies == null || !dependencies.IsValid() || values == null) return;

        var force = Vector3.up * jumpForce;
        var input = dependencies.Actions!.Gameplay.Move.ReadValue<Vector2>();
        if (input.magnitude > 0)
        {
            input = input.normalized;
            var added = new Vector3(input.x, 0, input.y) * values.jumpMomentum;
            added = Quaternion.Euler(0, values.GetCameraAngle(), 0) * added;
            force += added;
        }
        dependencies.rigidbody!.AddForce(force, ForceMode.Impulse);
        dependencies.rigidbody!.drag = 0;
    }
}