#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpecialState : StateNode
{
    [SerializeField]
    private AttackInfo specialAttack;
    
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
        
        var mousePos = Mouse.current.position.ReadValue();
        var center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        var angle = Vector2.SignedAngle(Vector2.right, mousePos - center);
        if (specialAttack.TryAttack(dependencies.entity!, dependencies.entity!.transform.position, angle))
        {
            cooldownFinished = Time.time + cooldown;
            delayFinished = Time.time + delay;
            SetBlocking(true);
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
