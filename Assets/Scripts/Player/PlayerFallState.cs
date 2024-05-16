#nullable enable
using UnityEngine;

public class PlayerFallState : MonoState
{
    [SerializeField]
    protected ActionsVariable? actionsVar;
    
    [SerializeField]
    protected new Rigidbody? rigidbody;
    
    [SerializeField]
    protected GroundChecker? groundChecker;
    
    [SerializeField]
    protected PlayerDashState? dashState;
    
    [SerializeField]
    protected PlayerWalkState? walkState;
    
    [SerializeField]
    protected PlayerSprintState? sprintState;
    
    public override void OnEnter(MonoStateMachine stateMachine)
    {
        if (rigidbody == null) return;

        rigidbody.drag = 0;
    }

    public override void OnTick(MonoStateMachine stateMachine, float delta)
    {
        if (actionsVar == null) return;
        if (groundChecker == null) return;
        if (dashState == null) return;
        if (walkState == null) return;
        if (sprintState == null) return;

        var gameplayActions = actionsVar.Provide().Gameplay;
        
        if (gameplayActions.Dash.WasPressedThisFrame())
        {
            stateMachine.SetState(dashState);
            return;
        }
        
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
    }
}