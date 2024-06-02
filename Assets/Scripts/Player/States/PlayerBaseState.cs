#nullable enable
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
        var values = GetBlackboard<PlayerValues>();
        if (dependencies == null || !dependencies.IsValid() || values == null) return;

        // Camera angle
        var angles = transform.localEulerAngles;
        angles.y = values.GetCameraAngle();
        transform.localEulerAngles = angles;
        
        // Camera position
        var cam = dependencies.camVar!.Provide()!;
        var pos = new Vector3(0, 0, -values.GetCameraDistance());
        pos = Quaternion.Euler(values.GetCameraElevation(), 0, 0) * pos;
        cam.transform.localPosition = pos;
        cam.transform.localEulerAngles = new Vector3(values.GetCameraElevation(), 0, 0);
        
        // Hotbar scrolling
        var num = dependencies.hotbarVar!.Provide().GetMaxSlots();
        var scroll = dependencies.Actions!.Gameplay.HotbarScroll.ReadValue<float>() / 10;
        selectedIndex = MathUtils.Mod(selectedIndex + scroll, num);
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