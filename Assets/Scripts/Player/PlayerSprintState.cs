#nullable enable
using UnityEngine;

public class PlayerSprintState : PlayerMovementState
{
    [SerializeField]
    protected PlayerWalkState? walkState;

    public override void OnTick(MonoStateMachine stateMachine, float delta)
    {
        base.OnTick(stateMachine, delta);

        if (actionsVar == null) return;
        if (walkState == null) return;

        var gameplayActions = actionsVar.Provide().Gameplay;
        
        if (stateMachine.GetState() == this && !gameplayActions.Sprint.IsPressed())
        {
            stateMachine.SetState(walkState);
        }
    }
}
