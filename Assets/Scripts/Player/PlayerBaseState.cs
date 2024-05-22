#nullable enable
using System;
using UnityEngine;

public class PlayerBaseState : StateNode
{
    [SerializeField]
    private PlayerActionState? actionState;
    
    [SerializeField]
    private PlayerSpecialState? specialState;
    
    [SerializeField]
    private PlayerMovementState? movementState;
    
    [SerializeField]
    private PlayerJumpState? jumpState;
    
    [SerializeField]
    private PlayerDashState? dashState;
    
    [SerializeField]
    private PlayerFallState? fallState;

    [SerializeField]
    private float selectedIndex;

    private void Start()
    {
        var dependencies = GetBlackboard<PlayerDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;
        
        dependencies.hotbarSubsystem!.OnSlotSelected(dependencies.hotbarVar!.Provide(), (int)selectedIndex);
    }

    private void Update()
    {
        OnTick(Time.deltaTime);
    }

    protected override void OnTick(float delta)
    {
        base.OnTick(delta);
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;
        
        var num = dependencies.hotbarVar!.Provide().GetMaxSlots();
        var scroll = dependencies.Actions!.Gameplay.HotbarScroll.ReadValue<float>() / 10;
        selectedIndex = (selectedIndex + scroll + num) % num;
        dependencies.hotbarSubsystem!.OnSlotSelected(dependencies.hotbarVar!.Provide(), (int)selectedIndex);

        // Blocking states automatically handled by SetCurrent
        
        if (dependencies.Actions!.Gameplay.Special.WasPressedThisFrame())
        {
            if (specialState != null && specialState.IsAvailable())
            {
                SetCurrent(specialState);
                return;
            }
        }

        if (dependencies.Actions!.Gameplay.Action.WasPressedThisFrame())
        {
            if (actionState != null && actionState.IsAvailable())
            {
                SetCurrent(actionState);
                return;
            }
        }
        
        if (dependencies.Actions!.Gameplay.Dash.WasPressedThisFrame())
        {
            SetCurrent(dashState);
            return;
        }
        
        if (dependencies.groundChecker!.IsGrounded())
        {
            if (dependencies.Actions!.Gameplay.Jump.WasPressedThisFrame())
            {
                SetCurrent(jumpState);
                return;
            }
            
            SetCurrent(movementState);
        }
        else
        {
            SetCurrent(fallState);
        }
    }
}