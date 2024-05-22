#nullable enable
using UnityEngine;

public class PlayerMovementState : StateNode
{
    [SerializeField]
    private PlayerWalkState? walkState;
    
    [SerializeField]
    private PlayerSprintState? sprintState;

    protected override void OnEnter()
    {
        base.OnEnter();
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;

        dependencies.rigidbody!.drag = 1;
    }

    protected override void OnTick(float delta)
    {
        base.OnTick(delta);
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;
        
        if (dependencies.Actions!.Gameplay.Sprint.IsPressed())
        {
            SetCurrent(sprintState);
        }
        else
        {
            SetCurrent(walkState);
        }
    }
}