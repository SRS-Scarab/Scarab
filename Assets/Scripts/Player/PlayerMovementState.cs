#nullable enable
using UnityEngine;

public class PlayerMovementState : MonoState
{
    [SerializeField]
    private PlayerWalkState? walkState;
    
    [SerializeField]
    private PlayerSprintState? sprintState;

    public override void OnEnter(MonoStateMachine stateMachine)
    {
        base.OnEnter(stateMachine);
        
        var blackboard = stateMachine.GetBlackboard<PlayerBlackboard>();
        if (blackboard == null || !blackboard.IsValid()) return;
        
        if (blackboard.Actions!.Gameplay.Sprint.IsPressed())
        {
            stateMachine.SetState(sprintState);
        }
        else
        {
            stateMachine.SetState(walkState);
        }
    }

    protected override void OnEnterPropagate(MonoStateMachine stateMachine)
    {
        base.OnEnterPropagate(stateMachine);
        
        var blackboard = stateMachine.GetBlackboard<PlayerBlackboard>();
        if (blackboard == null || !blackboard.IsValid()) return;

        blackboard.rigidbody!.drag = 1;
    }
}