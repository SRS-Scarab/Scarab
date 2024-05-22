#nullable enable
using UnityEngine;

public class PlayerBaseState : MonoState
{
    [SerializeField]
    private PlayerMovementState? movementState;
    
    [SerializeField]
    private PlayerJumpState? jumpState;
    
    [SerializeField]
    private PlayerDashState? dashState;
    
    [SerializeField]
    private PlayerFallState? fallState;

    protected override void OnTickPropagate(MonoStateMachine stateMachine, float delta)
    {
        base.OnTickPropagate(stateMachine, delta);
        
        var blackboard = stateMachine.GetBlackboard<PlayerDependencyBlackboard>();
        if (blackboard == null || !blackboard.IsValid()) return;

        if (blackboard.Actions!.Gameplay.Dash.WasPressedThisFrame())
        {
            stateMachine.SetState(dashState);
            return;
        }
        
        if (blackboard.groundChecker!.IsGrounded())
        {
            if (blackboard.Actions!.Gameplay.Jump.WasPressedThisFrame())
            {
                stateMachine.SetState(jumpState);
                return;
            }
            
            stateMachine.SetState(movementState);
        }
        else
        {
            stateMachine.SetState(fallState);
        }
    }
}