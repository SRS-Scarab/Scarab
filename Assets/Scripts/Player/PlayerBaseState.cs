#nullable enable
using UnityEngine;

public class PlayerBaseState : StateNode
{
    [SerializeField]
    private PlayerMovementState? movementState;
    
    [SerializeField]
    private PlayerJumpState? jumpState;
    
    [SerializeField]
    private PlayerDashState? dashState;
    
    [SerializeField]
    private PlayerFallState? fallState;

    protected override void OnTick(float delta)
    {
        base.OnTick(delta);
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;

        if (GetCurrent() == dashState && dashState != null)
        {
            if (!dashState.IsFinished())
            {
                return;
            }
        }

        if (dependencies.Actions!.Gameplay.Dash.WasPressedThisFrame())
        {
            SetCurrent(dashState);
            return;
        }
        
        if (dependencies.groundChecker!.IsGrounded())
        {
            if (dependencies.Actions!.Gameplay.Jump.WasPressedThisFrame())
            {
                SetCurrent(jumpState);
                return;
            }
            
            SetCurrent(movementState);
        }
        else
        {
            SetCurrent(fallState);
        }
    }
}