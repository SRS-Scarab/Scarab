#nullable enable
using UnityEngine;

public class PlayerJumpState : MonoState
{
    [SerializeField]
    protected float force;
    
    [SerializeField]
    protected new Rigidbody? rigidbody;
    
    [SerializeField]
    protected PlayerFallState? fallState;

    public override void OnEnter(MonoStateMachine stateMachine)
    {
        if (rigidbody == null) return;

        rigidbody.drag = 0;
        rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    public override void OnTick(MonoStateMachine stateMachine, float delta)
    {
        if (fallState == null) return;

        stateMachine.SetState(fallState);
    }
}