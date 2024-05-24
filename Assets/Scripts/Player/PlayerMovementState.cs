#nullable enable
using System.Linq;
using UnityEngine;

public class PlayerMovementState : StateNode
{
    [SerializeField]
    private float stepDistance = 0.6f;
    
    [SerializeField]
    private float stepHeight = 0.3f;

    [SerializeField]
    private float stepSpeed = 2;
    
    [SerializeField]
    private PlayerWalkState? walkState;
    
    [SerializeField]
    private PlayerSprintState? sprintState;

    protected override void OnEnter()
    {
        base.OnEnter();
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;

        dependencies.rigidbody!.drag = 3;
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

        var velocity = dependencies.rigidbody!.velocity;
        if (velocity.sqrMagnitude != 0)
        {
            var lower = dependencies.feetPosition!.transform.position;
            var upper = lower + new Vector3(0, stepHeight, 0);

            var lowerHits = Physics.RaycastAll(lower, velocity, stepDistance);
            if (lowerHits.Any(e => e.rigidbody != dependencies.rigidbody))
            {
                var upperHits = Physics.RaycastAll(upper, velocity, stepDistance * 1.5f);
                if (upperHits.All(e => e.rigidbody == dependencies.rigidbody))
                {
                    dependencies.rigidbody!.position += new Vector3(0, stepHeight * delta, 0);
                }
            }
        }
    }
}