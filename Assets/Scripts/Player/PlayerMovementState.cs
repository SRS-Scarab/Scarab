#nullable enable
using UnityEngine;

public class PlayerMovementState : MonoState
{
    [SerializeField]
    protected float speed;
    
    [SerializeField]
    protected ActionsVariable? actionsVar;
    
    [SerializeField]
    protected new Rigidbody? rigidbody;
    
    [SerializeField]
    protected GroundChecker? groundChecker;
    
    [SerializeField]
    protected PlayerFallState? fallState;
    
    [SerializeField]
    protected PlayerJumpState? jumpState;
    
    [SerializeField]
    protected PlayerDashState? dashState;

    public override void OnEnter(MonoStateMachine stateMachine)
    {
        if (rigidbody == null) return;

        rigidbody.drag = 1;
    }

    public override void OnTick(MonoStateMachine stateMachine, float delta)
    {
        if (actionsVar == null) return;
        if (rigidbody == null) return;
        if (groundChecker == null) return;
        if (jumpState == null) return;
        if (dashState == null) return;

        var gameplayActions = actionsVar.Provide().Gameplay;

        var velocity = rigidbody.velocity;
        var input = gameplayActions.Move.ReadValue<Vector2>().normalized;
        velocity.x = input.x * speed;
        velocity.z = input.y * speed;
        rigidbody.velocity = velocity;

        if (!groundChecker.IsGrounded())
        {
            stateMachine.SetState(fallState);
            return;
        }

        if (gameplayActions.Jump.WasPressedThisFrame())
        {
            stateMachine.SetState(jumpState);
            return;
        }
        
        if (gameplayActions.Dash.WasPressedThisFrame())
        {
            stateMachine.SetState(dashState);
        }
    }
}