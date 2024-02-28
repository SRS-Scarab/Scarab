#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MovementStateMachine
{
    private Actions Actions => actionsVar.Provide();
    
    [SerializeField]
    private ActionsVariable actionsVar = null!;
    
    [SerializeField]
    private new Rigidbody rigidbody = null!;

    [SerializeField]
    private float walkSpeed = 5;
    
    [SerializeField]
    private float sprintSpeed = 10;
    
    [SerializeField]
    private float jumpForce = 5;

    private bool IsGrounded => groundedCounter > 0;
    
    [SerializeField]
    private float groundedCounter;

    [SerializeField]
    private bool isSprinting;

    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody>();
        
        Actions.Gameplay.Sprint.performed += OnBeginSprint;
        Actions.Gameplay.Sprint.canceled += OnEndSprint;
        Actions.Gameplay.Jump.performed += OnJump;
    }

    protected override MovementState GetInitialState()
    {
        return new FallState(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        groundedCounter++;
    }

    private void OnCollisionExit(Collision other)
    {
        groundedCounter--;
    }

    private void OnBeginSprint(InputAction.CallbackContext context)
    {
        isSprinting = true;
    }

    private void OnEndSprint(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded)
        {
            SetState(new JumpState(this));
        }
    }

    private class WalkState : MovementState<PlayerStateMachine>
    {
        private readonly InputAction _moveAction;
        
        public WalkState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            _moveAction = StateMachine.Actions.Gameplay.Move;
        }
        
        public override MovementState Tick()
        {
            var velocity = StateMachine.rigidbody.velocity;
            var input = _moveAction.ReadValue<Vector2>().normalized * StateMachine.walkSpeed;
            velocity.x = input.x;
            velocity.z = input.y;
            StateMachine.rigidbody.velocity = velocity;

            if (StateMachine.isSprinting)
            {
                return new SprintState(StateMachine);
            }

            return this;
        }
    }
    
    private class SprintState : MovementState<PlayerStateMachine>
    {
        private readonly InputAction _moveAction;
        
        public SprintState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            _moveAction = StateMachine.Actions.Gameplay.Move;
        }
        
        public override MovementState Tick()
        {
            var velocity = StateMachine.rigidbody.velocity;
            var input = _moveAction.ReadValue<Vector2>().normalized * StateMachine.sprintSpeed;
            velocity.x = input.x;
            velocity.z = input.y;
            StateMachine.rigidbody.velocity = velocity;
            
            if (!StateMachine.isSprinting)
            {
                return new WalkState(StateMachine);
            }

            return this;
        }
    }
    
    private class JumpState : MovementState<PlayerStateMachine>
    {
        public JumpState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            StateMachine.rigidbody.AddForce(Vector3.up * StateMachine.jumpForce, ForceMode.Impulse);
        }
        
        public override MovementState Tick()
        {
            if (StateMachine.rigidbody.velocity.y < 0)
            {
                return new FallState(StateMachine);
            }

            return this;
        }
    }
    
    private class FallState : MovementState<PlayerStateMachine>
    {
        public FallState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override MovementState Tick()
        {
            if (StateMachine.IsGrounded)
            {
                return StateMachine.isSprinting ? new SprintState(StateMachine) : new WalkState(StateMachine);
            }

            return this;
        }
    }
    
    private class DashState : MovementState<PlayerStateMachine>
    {
        public DashState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }
        
        public override MovementState Tick()
        {
            var groundSpeed = StateMachine.rigidbody.velocity;
            groundSpeed.y = 0;
            if (groundSpeed.magnitude < 1)
            {
                if (StateMachine.IsGrounded)
                {
                    return StateMachine.isSprinting ? new SprintState(StateMachine) : new WalkState(StateMachine);
                }

                return new FallState(StateMachine);
            }

            return this;
        }
    }
}
