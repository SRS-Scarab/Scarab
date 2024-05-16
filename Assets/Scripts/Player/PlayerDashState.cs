#nullable enable
using UnityEngine;

public class PlayerDashState : MonoState
{
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float threshold;
    
    [SerializeField]
    protected ActionsVariable? actionsVar;
    
    [SerializeField]
    protected new Rigidbody? rigidbody;
    
    [SerializeField]
    protected GroundChecker? groundChecker;
    
    [SerializeField]
    protected PlayerWalkState? walkState;
    
    [SerializeField]
    protected PlayerSprintState? sprintState;
    
    [SerializeField]
    protected PlayerFallState? fallState;

    public override void OnEnter(MonoStateMachine stateMachine)
    {
        if (actionsVar == null) return;
        if (rigidbody == null) return;
        
        var gameplayActions = actionsVar.Provide().Gameplay;

        var input = gameplayActions.Move.ReadValue<Vector2>().normalized;

        rigidbody.useGravity = false;
        rigidbody.drag = 3;
        rigidbody.velocity = new Vector3(input.x * speed, 0, input.y * speed);
    }

    public override void OnTick(MonoStateMachine stateMachine, float delta)
    {
        if (actionsVar == null) return;
        if (rigidbody == null) return;
        if (groundChecker == null) return;
        if (walkState == null) return;
        if (sprintState == null) return;
        if (fallState == null) return;
        
        var gameplayActions = actionsVar.Provide().Gameplay;
        
        var groundSpeed = rigidbody.velocity;
        groundSpeed.y = 0;
        if (groundSpeed.magnitude < threshold)
        {
            if (groundChecker.IsGrounded())
            {
                if (gameplayActions.Sprint.IsPressed())
                {
                    stateMachine.SetState(sprintState);
                }
                else
                {
                    stateMachine.SetState(walkState);
                }
            }
            else
            {
                stateMachine.SetState(fallState);
            }
        }
    }

    public override void OnExit(MonoStateMachine stateMachine)
    {
        if (rigidbody == null) return;

        rigidbody.useGravity = true;
    }
}