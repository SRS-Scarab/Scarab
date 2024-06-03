#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionState : StateNode
{
    [SerializeField]
    private AttackPrefab? basicAttack;
    
    [SerializeField]
    private float cooldown;
    
    [SerializeField]
    private float cooldownFinished;
    
    [SerializeField]
    private float delay;

    [SerializeField]
    private float delayFinished;

    public bool IsAvailable()
    {
        return Time.time >= cooldownFinished;
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        
        var dependencies = GetBlackboard<PlayerDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;

        var hotbar = dependencies.hotbarSubsystem!.GetSelectedInventory()!;
        var index = dependencies.hotbarSubsystem!.GetSelectedIndex();
        var stack = hotbar.GetStack(index);

        if (stack.itemType == null)
        {
            var mousePos = Mouse.current.position.ReadValue();
            var center = new Vector2(Screen.width / 2f, Screen.height / 2f);
            var angle = -Vector2.SignedAngle(Vector2.right, mousePos - center);
            if (basicAttack != null && basicAttack.TryInstantiate(dependencies.entity!, dependencies.entity!.transform.position, angle))
            {
                cooldownFinished = Time.time + cooldown;
                delayFinished = Time.time + delay;
                SetBlocking(true);
            }
        }
        else
        {
            stack.itemType.OnItemUse(dependencies.entity!, hotbar, index);
            cooldownFinished = Time.time + cooldown;
        }
    }

    protected override void OnTick(float delta)
    {
        base.OnTick(delta);

        if (Time.time >= delayFinished)
        {
            SetBlocking(false);
        }
    }
}