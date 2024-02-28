#nullable enable
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2D : StateMachine
{
    private Actions Actions => actionsVar.Provide();
    
    [SerializeField]
    private ActionsVariable actionsVar = null!;
    
    [SerializeField]
    private new Rigidbody2D rigidbody = null!;

    [SerializeField]
    private float walkSpeed = 5;
    
    [SerializeField]
    private float sprintSpeed = 10;
    
    [SerializeField]
    private float dashForce = 10;
    
    [SerializeField]
    private float dashDecay = 3;
    
    [SerializeField]
    private bool isSprinting;
    
    [SerializeField]
    private Vector2 moveDirection = Vector2.right;
    
    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody2D>();
        
        Actions.Gameplay.Sprint.performed += OnBeginSprint;
        Actions.Gameplay.Sprint.canceled += OnEndSprint;
        Actions.Gameplay.Dash.performed += OnDash;
    }

    protected override State GetInitialState()
    {
        return new WalkState(this);
    }
    
    private void OnBeginSprint(InputAction.CallbackContext context)
    {
        isSprinting = true;
    }

    private void OnEndSprint(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }
    
    private void OnDash(InputAction.CallbackContext context)
    {
        if (Current is not DashState)
        {
            Current = new DashState(this);
        }
    }

    private class WalkState : State<PlayerMovement2D>
    {
        private readonly InputAction _moveAction;
        
        public WalkState(PlayerMovement2D stateMachine) : base(stateMachine)
        {
            _moveAction = StateMachine.Actions.Gameplay.Move;
        }
        
        public override State Tick()
        {
            var input = _moveAction.ReadValue<Vector2>().normalized;
            StateMachine.rigidbody.velocity = input * StateMachine.walkSpeed;
            if(input != Vector2.zero) StateMachine.moveDirection = input;

            if (StateMachine.isSprinting)
            {
                return new SprintState(StateMachine);
            }

            return this;
        }
    }
    
    private class SprintState : State<PlayerMovement2D>
    {
        private readonly InputAction _moveAction;
        
        public SprintState(PlayerMovement2D stateMachine) : base(stateMachine)
        {
            _moveAction = StateMachine.Actions.Gameplay.Move;
        }
        
        public override State Tick()
        {
            var input = _moveAction.ReadValue<Vector2>().normalized;
            StateMachine.rigidbody.velocity = input * StateMachine.sprintSpeed;
            if(input != Vector2.zero) StateMachine.moveDirection = input;
            
            if (!StateMachine.isSprinting)
            {
                return new WalkState(StateMachine);
            }

            return this;
        }
    }
    
    [Serializable]
    private class DashState : State<PlayerMovement2D>
    {
        [SerializeField]
        private float prevDrag;
        
        public DashState(PlayerMovement2D stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            prevDrag = StateMachine.rigidbody.drag;
            StateMachine.rigidbody.drag = StateMachine.dashDecay;
            StateMachine.rigidbody.AddForce(StateMachine.moveDirection * StateMachine.dashForce, ForceMode2D.Impulse);
        }

        public override void Exit()
        {
            StateMachine.rigidbody.drag = prevDrag;
        }

        public override State Tick()
        {
            if (StateMachine.rigidbody.velocity.magnitude <= StateMachine.walkSpeed)
            {
                return StateMachine.isSprinting ? new SprintState(StateMachine) : new WalkState(StateMachine);
            }

            return this;
        }
    }
}
