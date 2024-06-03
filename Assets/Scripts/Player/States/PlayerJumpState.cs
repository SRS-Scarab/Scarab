#nullable enable
using UnityEngine;

public class PlayerJumpState : StateNode
{
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private AudioClip jumpClip;

    protected override void OnEnter()
    {
        base.OnEnter();
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        var values = GetBlackboard<PlayerValues>();
        if (dependencies == null || !dependencies.IsValid() || values == null) return;

        var force = Vector3.up * jumpForce;
        var added = values.moveVelocity;
        added.y = 0;
        force += added;
        dependencies.rigidbody!.AddForce(force, ForceMode.Impulse);
        dependencies.rigidbody!.drag = 0;
        SoundFXManager.instance.PlaySound(jumpClip, transform.position, 1f);
    }
}