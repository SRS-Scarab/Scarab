#nullable enable
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionState : StateNode
{
    [SerializeField]
    private AttackPrefab? basicAttack;

    [SerializeField]
    private AudioClip[] attackClips;

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

    public float GetRechargeProgress()
    {
        return 1 - Mathf.Clamp01((cooldownFinished - Time.time) / cooldown);
    }

    private void Start()
    {
        var dependencies = GetBlackboard<PlayerDependencies>();
        if (dependencies == null || !dependencies.IsValid()) return;

        dependencies.proxy!.actionState = this;
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
            if (basicAttack != null && basicAttack.TryInstantiate(dependencies.entity!, dependencies.attackPosition!.transform.position, angle))
            {
                SoundFXManager.instance.PlayRandomSound(attackClips, transform, 1f);
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