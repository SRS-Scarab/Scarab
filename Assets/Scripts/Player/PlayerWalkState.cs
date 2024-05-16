#nullable enable
using UnityEngine;

public class PlayerWalkState : PlayerMovementState
{
    [SerializeField]
    protected PlayerSprintState? sprintState;

    public override void OnTick(MonoStateMachine stateMachine, float delta)
    {
        base.OnTick(stateMachine, delta);

        if (actionsVar == null) return;
        if (sprintState == null) return;

        var gameplayActions = actionsVar.Provide().Gameplay;
        
        if (stateMachine.GetState() == this && gameplayActions.Sprint.IsPressed())
        {
            stateMachine.SetState(sprintState);
        }
    }
}
