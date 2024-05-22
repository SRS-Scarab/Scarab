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
        if (dependencies == null || !dependencies.IsValid()) return;

        dependencies.rigidbody!.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        dependencies.rigidbody!.drag = 0;
    }
}