#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : StateMachine
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

    protected override State GetInitialState()
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
            Current = new JumpState(this);
        }
    }

    private class WalkState : State<PlayerBehavior>
    {
        private readonly InputAction _moveAction;
        
        public WalkState(PlayerBehavior stateMachine) : base(stateMachine)
        {
            _moveAction = StateMachine.Actions.Gameplay.Move;
        }
        
        public override void OnTick()
        {
            var velocity = StateMachine.rigidbody.velocity;
            var input = _moveAction.ReadValue<Vector2>().normalized * StateMachine.walkSpeed;
            velocity.x = input.x;
            velocity.z = input.y;
            StateMachine.rigidbody.velocity = velocity;

            if (StateMachine.isSprinting)
            {
                StateMachine.Current = new SprintState(StateMachine);
            }
        }
    }
    
    private class SprintState : State<PlayerBehavior>
    {
        private readonly InputAction _moveAction;
        
        public SprintState(PlayerBehavior stateMachine) : base(stateMachine)
        {
            _moveAction = StateMachine.Actions.Gameplay.Move;
        }
        
        public override void OnTick()
        {
            var velocity = StateMachine.rigidbody.velocity;
            var input = _moveAction.ReadValue<Vector2>().normalized * StateMachine.sprintSpeed;
            velocity.x = input.x;
            velocity.z = input.y;
            StateMachine.rigidbody.velocity = velocity;
            
            if (!StateMachine.isSprinting)
            {
                StateMachine.Current = new WalkState(StateMachine);
            }
        }
    }
    
    private class JumpState : State<PlayerBehavior>
    {
        public JumpState(PlayerBehavior stateMachine) : base(stateMachine)
        {
            StateMachine.rigidbody.AddForce(Vector3.up * StateMachine.jumpForce, ForceMode.Impulse);
        }
        
        public override void OnTick()
        {
            if (StateMachine.rigidbody.velocity.y < 0)
            {
                StateMachine.Current = new FallState(StateMachine);
            }
        }
    }
    
    private class FallState : State<PlayerBehavior>
    {
        public FallState(PlayerBehavior stateMachine) : base(stateMachine)
        {
        }
        
        public override void OnTick()
        {
            if (StateMachine.IsGrounded)
            {
                StateMachine.Current = StateMachine.isSprinting ? new SprintState(StateMachine) : new WalkState(StateMachine);
            }
        }
    }
    
    private class DashState : State<PlayerBehavior>
    {
        public DashState(PlayerBehavior stateMachine) : base(stateMachine)
        {
        }
        
        public override void OnTick()
        {
            var groundSpeed = StateMachine.rigidbody.velocity;
            groundSpeed.y = 0;
            if (groundSpeed.magnitude < 1)
            {
                if (StateMachine.IsGrounded)
                {
                    StateMachine.Current = StateMachine.isSprinting ? new SprintState(StateMachine) : new WalkState(StateMachine);
                    return;
                }

                StateMachine.Current = new FallState(StateMachine);
            }
        }
    }
}
