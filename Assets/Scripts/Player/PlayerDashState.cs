#nullable enable
using UnityEngine;

public class PlayerDashState : StateNode
{
    [SerializeField]
    private float dashForce;

    [SerializeField]
    private float threshold;

    protected override void OnEnter()
    {
        base.OnEnter();
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;

        var input = dependencies.Actions!.Gameplay.Move.ReadValue<Vector2>().normalized;

        dependencies.rigidbody!.useGravity = false;
        dependencies.rigidbody.drag = 3;
        dependencies.rigidbody.AddForce(new Vector3(input.x * dashForce, 0, input.y * dashForce), ForceMode.Impulse);

        SetBlocking(true);
    }

    protected override void OnTick(float delta)
    {
        base.OnTick(delta);
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;
        
        var groundSpeed = dependencies.rigidbody!.velocity;
        groundSpeed.y = 0;
        if (groundSpeed.magnitude < threshold)
        {
            SetBlocking(false);
        }
    }

    protected override void OnExit()
    {
        base.OnExit();
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;

        dependencies.rigidbody!.useGravity = true;
    }
}