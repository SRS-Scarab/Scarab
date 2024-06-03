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
    private TextureAnimationClip? idle;
    
    [SerializeField]
    private TextureAnimationClip? left;
    
    [SerializeField]
    private TextureAnimationClip? right;
    
    [SerializeField]
    private TextureAnimationClip? up;
    
    [SerializeField]
    private TextureAnimationClip? down;
    
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
        var values = GetBlackboard<PlayerValues>();
        if (dependencies == null || !dependencies.IsValid() || values == null) return;
        
        if (dependencies.Actions!.Gameplay.Sprint.IsPressed())
        {
            SetCurrent(sprintState);
        }
        else
        {
            SetCurrent(walkState);
        }
        
        var input = dependencies.Actions!.Gameplay.Move.ReadValue<Vector2>();
        if (input.sqrMagnitude == 0)
        {
            dependencies.animator!.current = idle;
        }
        else
        {
            if (input.x == 0)
            {
                dependencies.animator!.current = input.y > 0 ? up : down;
            }
            else
            {
                dependencies.animator!.current = input.x > 0 ? right : left;
            }
        }

        if (values.moveVelocity.sqrMagnitude != 0)
        {
            var lower = dependencies.feetPosition!.transform.position;
            var upper = lower + new Vector3(0, stepHeight, 0);

            var lowerHits = Physics.RaycastAll(lower, values.moveVelocity, stepDistance);
            if (lowerHits.Any(e => e.rigidbody != dependencies.rigidbody))
            {
                var upperHits = Physics.RaycastAll(upper, values.moveVelocity, stepDistance * 1.5f);
                if (upperHits.All(e => e.rigidbody == dependencies.rigidbody))
                {
                    dependencies.rigidbody!.position += new Vector3(0, stepHeight * delta, 0);
                }
            }
        }
    }
}