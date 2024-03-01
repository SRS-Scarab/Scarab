#nullable enable
using System;
using UnityEngine;
using UnityEngine.Assertions;

public class MinibossBehavior : StateMachine
{
    [SerializeField]
    private CombatEntityVariable playerVar = null!;
    
    [SerializeField]
    private CombatEntity entity = null!;
    
    [SerializeField]
    private new Rigidbody2D rigidbody = null!;
    
    [SerializeField]
    private float walkSpeed = 2;
    
    [SerializeField]
    private float followRange = 15;
    
    [SerializeField]
    private float dashDistance = 12;
    
    [SerializeField]
    private float maxDistance = 8;
    
    [SerializeField]
    private float minDistance = 4;

    [SerializeField]
    private NpcAttackInfo basicAttack;
    
    [SerializeField]
    private NpcAttackInfo sweepAttack;
    
    [SerializeField]
    private NpcAttackInfo pierceAttack;
    
    [SerializeField]
    private NpcAttackInfo groundAttack;
    
    [SerializeField]
    private float attackFollowThrough = 0.5f;
    
    [SerializeField]
    private float dashForce = 10;
    
    [SerializeField]
    private float dashDecay = 3;
    
    [SerializeField]
    private float dashCooldown = 5;
    
    [SerializeField]
    private float dashCooldownRemaining;
    
    protected override void Start()
    {
        base.Start();
        entity = GetComponent<CombatEntity>();
        rigidbody = GetComponent<Rigidbody2D>();

        AssertDependencies();
    }

    protected override void Update()
    {
        basicAttack.OnTick();
        sweepAttack.OnTick();
        pierceAttack.OnTick();
        groundAttack.OnTick();
        dashCooldownRemaining -= Time.deltaTime;
        
        base.Update();
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

    private NpcAttackInfo? GetNextAttack()
    {
        var playerEntity = playerVar.Provide();
        if (playerEntity == null) return null;

        if (pierceAttack.CanAttack(entity, playerEntity!) && IsPlayerWithinRange(pierceAttack.range))
        {
            return pierceAttack;
        }
        
        if (sweepAttack.CanAttack(entity, playerEntity!) && IsPlayerWithinRange(sweepAttack.range))
        {
            return sweepAttack;
        }
        
        if (groundAttack.CanAttack(entity, playerEntity!) && IsPlayerWithinRange(groundAttack.range))
        {
            return groundAttack;
        }
        
        if (basicAttack.CanAttack(entity, playerEntity!) && IsPlayerWithinRange(basicAttack.range))
        {
            return basicAttack;
        }

        return null;
    }
    
    private void AssertDependencies()
    {
        Assert.IsNotNull(playerVar);
        Assert.IsNotNull(entity);
        Assert.IsNotNull(rigidbody);
    }
    
    [Serializable]
    private class WaitState : State<MinibossBehavior>
    {
        public WaitState(MinibossBehavior stateMachine) : base(stateMachine)
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
    private class FollowState : State<MinibossBehavior>
    {
        [SerializeField]
        private bool isBacking;
        
        public FollowState(MinibossBehavior stateMachine) : base(stateMachine)
        {
        }

        public override void OnTick()
        {
            var playerEntity = StateMachine.playerVar.Provide();
            if (playerEntity == null) return;
            var curPos = StateMachine.transform.position;
            var playerPos = playerEntity!.transform.position;
            
            if (!StateMachine.IsPlayerWithinRange(StateMachine.followRange))
            {
                StateMachine.Current = new WaitState(StateMachine);
                return;
            }

            if (StateMachine.dashCooldownRemaining <= 0 && !StateMachine.IsPlayerWithinRange(StateMachine.dashDistance))
            {
                StateMachine.Current = new DashState(StateMachine, playerPos - curPos);
                return;
            }

            var attack = StateMachine.GetNextAttack();
            if (attack != null)
            {
                StateMachine.Current = new AttackState(StateMachine, attack);
                return;
            }

            if (StateMachine.IsPlayerWithinRange(StateMachine.minDistance))
            {
                isBacking = true;
            }
            else if (!StateMachine.IsPlayerWithinRange(StateMachine.maxDistance))
            {
                isBacking = false;
            }
            
            var step = StateMachine.walkSpeed * Time.deltaTime;
            if (isBacking)
            {
                step *= -1;
            }
            StateMachine.transform.position = Vector2.MoveTowards(curPos, playerPos, step);
        }
    }
    
    [Serializable]
    private class AttackState : State<MinibossBehavior>
    {
        [SerializeField]
        private NpcAttackInfo attackInfo;
        
        [SerializeField]
        private float followThroughRemaining;
        
        public AttackState(MinibossBehavior stateMachine, NpcAttackInfo attackInfo) : base(stateMachine)
        {
            this.attackInfo = attackInfo;
        }

        public override void OnEnter()
        {
            var playerEntity = StateMachine.playerVar.Provide();
            if (playerEntity == null) return;
            attackInfo.Attack(StateMachine.entity, playerEntity!);
            followThroughRemaining = StateMachine.attackFollowThrough;
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
    
    [Serializable]
    private class DashState : State<MinibossBehavior>
    {
        [SerializeField]
        private Vector2 direction;
        
        [SerializeField]
        private float prevDrag;
        
        public DashState(MinibossBehavior stateMachine, Vector2 direction) : base(stateMachine)
        {
            this.direction = direction.normalized;
        }

        public override void OnEnter()
        {
            StateMachine.dashCooldownRemaining = StateMachine.dashCooldown;
            prevDrag = StateMachine.rigidbody.drag;
            StateMachine.rigidbody.drag = StateMachine.dashDecay;
            StateMachine.rigidbody.AddForce(direction * StateMachine.dashForce, ForceMode2D.Impulse);
        }

        public override void OnExit()
        {
            StateMachine.rigidbody.drag = prevDrag;
        }

        public override void OnTick()
        {
            if (StateMachine.rigidbody.velocity.magnitude <= StateMachine.walkSpeed)
            {
                StateMachine.Current = new FollowState(StateMachine);
            }
        }
    }
}
