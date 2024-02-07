#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Dependencies")]

    [SerializeField] private InputSubsystem? inputSubsystem;
    [SerializeField] private ActionsVariable? actionsVar;
    [SerializeField] private InventorySubsystem? hotbarSubsystem;
    [SerializeField] private InventoryVariable? hotbarInventory;
    [SerializeField] private CameraVariable? camVar;
    [SerializeField] private CombatEntity? entity;

    [Header("Parameters")]

    [SerializeField] private AttackInfo basicAttack;
    [SerializeField] private AttackInfo specialAttack;
    [SerializeField] private float cooldown;
    [SerializeField] private float skillCooldown;

    [Header("State")]

    [SerializeField] private float cooldownLeft;
    [SerializeField] private float skillCooldownLeft;

    private void Start()
    {
        if (actionsVar != null)
        {
            actionsVar.Provide().Gameplay.Action.performed += OnAction;
            actionsVar.Provide().Gameplay.Special.performed += OnSpecial;
        }
        if (hotbarSubsystem != null && hotbarInventory != null) hotbarSubsystem.OnSlotSelected(hotbarInventory.Provide(), 0);
    }

    private void Update()
    {
        cooldownLeft -= Time.deltaTime;
        skillCooldownLeft -= Time.deltaTime;
        
        if(inputSubsystem != null) inputSubsystem.PollMouseStatus();
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        if (inputSubsystem != null && inputSubsystem.IsConsumedByInterface(context)) return;
        if (cooldownLeft <= 0)
        {
            // todo separate unified action cooldown into individual timers?
            if (hotbarSubsystem != null && entity != null)
            {
                var hotbar = hotbarSubsystem.GetSelectedInventory();
                if (hotbar != null)
                {
                    var stack = hotbar.GetStack(hotbarSubsystem.GetSelectedIndex());
                    if (stack.itemType == null)
                    {
                        if (camVar != null && camVar.Provide() != null)
                        {
                            var mousePos = camVar.Provide()!.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                            var playerPos = entity.transform.position;
                            var angle = Vector2.SignedAngle(Vector2.right, mousePos - playerPos);
                            basicAttack.Instantiate(entity, playerPos, angle);
                        }
                    }
                    else stack.itemType.OnItemUse(entity, hotbar, hotbarSubsystem.GetSelectedIndex());
                }
            }
            cooldownLeft = cooldown;
        }
    }

    private void OnSpecial(InputAction.CallbackContext context)
    {
        if (skillCooldownLeft <= 0)
        {
            if (entity != null)
            {
                if (camVar != null && camVar.Provide() != null)
                {
                    var mousePos = camVar.Provide()!.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    var playerPos = entity.transform.position;
                    var angle = Vector2.SignedAngle(Vector2.right, mousePos - playerPos);
                    specialAttack.Instantiate(entity, playerPos, angle);
                }
            }
            skillCooldownLeft = skillCooldown;
        }
    }
}
