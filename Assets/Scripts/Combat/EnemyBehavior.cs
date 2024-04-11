#nullable enable
using System;
using UnityEngine;
using UnityEngine.Assertions;
using Pathfinding;

public class EnemyBehavior : StateMachine
{
    [SerializeField]
    private CombatEntityVariable playerVar = null!;
    
    [SerializeField]
    private CombatEntity entity = null!;
    
    [SerializeField]
    private float walkSpeed = 3;
    
    [SerializeField]
    private float followRange = 5;
    
    [SerializeField]
    private AttackInfo attackInfo;
    
    [SerializeField]
    private float attackRange = 1.5f;
    
    [SerializeField]
    private float attackFollowThrough = 1.5f;
    private AIPath aipath;
    private AIDestinationSetter aiDestinationSetter;
    
    protected override void Start()
    {
        base.Start();
        entity = GetComponent<CombatEntity>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aipath = GetComponent<AIPath>();

        AssertDependencies();
    }
    
    protected override State GetInitialState()
    {
        return new WaitState(this);
    }

    private bool IsPlayerWithinRange(float range)
    {
        var playerEntity = playerVar.Provide();
        if (playerEntity == null) return false;
        return (transform.position - playerEntity!.transform.position).sqrMagnitude <= range * range;
    }
    
    private void AssertDependencies()
    {
        Assert.IsNotNull(playerVar);
        Assert.IsNotNull(entity);
    }

    [Serializable]
    private class WaitState : State<EnemyBehavior>
    {
        public WaitState(EnemyBehavior stateMachine) : base(stateMachine)
        {
        }

        public override void OnTick()
        {
            if (StateMachine.IsPlayerWithinRange(StateMachine.followRange))
            {
                StateMachine.Current = new FollowState(StateMachine);
            }
        }
    }
    
    [Serializable]
    private class FollowState : State<EnemyBehavior>
    {
        public FollowState(EnemyBehavior stateMachine) : base(stateMachine)
        {
        }

        public override void OnTick()
        {
            if (!StateMachine.IsPlayerWithinRange(StateMachine.followRange))
            {
                StateMachine.Current = new WaitState(StateMachine);
                return;
            }
            
            if (StateMachine.IsPlayerWithinRange(StateMachine.attackRange))
            {
                StateMachine.Current = new AttackState(StateMachine);
                return;
            }
            
            var curPos = StateMachine.transform.position;
            var playerEntity = StateMachine.playerVar.Provide();
            if (playerEntity == null) return;
            StateMachine.aiDestinationSetter.target = playerEntity.transform;
            StateMachine.aipath.maxSpeed = StateMachine.walkSpeed;
        }
    }

    [Serializable]
    private class AttackState : State<EnemyBehavior>
    {
        [SerializeField]
        private float followThroughRemaining;
        
        public AttackState(EnemyBehavior stateMachine) : base(stateMachine)
        {
        }

        public override void OnEnter()
        {
            var playerEntity = StateMachine.playerVar.Provide();
            if (playerEntity == null) return;
            var rotation = Vector2.SignedAngle(Vector2.right, playerEntity!.transform.position - StateMachine.transform.position);
            if (StateMachine.attackInfo.TryAttack(StateMachine.entity, StateMachine.entity.transform.position, rotation))
            {
                followThroughRemaining = StateMachine.attackFollowThrough;
            }
        }

        public override void OnTick()
        {
            followThroughRemaining -= Time.deltaTime;
            
            if (followThroughRemaining <= 0)
            {
                StateMachine.Current = new FollowState(StateMachine);
            }
        }
    }
}
