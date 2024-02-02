#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Dependencies")]

    [SerializeField] private ActionsVariable? actionsVar;
    [SerializeField] private CombatEntity? entity;

    [Header("Parameters")]

    [SerializeField] private AttackInfo basicAttack;
    [SerializeField] private AttackInfo specialAttack;
    [SerializeField] private float cooldown;
    [SerializeField] private float skillCooldown;

    [Header("State")]

    [SerializeField] private float rotation;
    [SerializeField] private float cooldownLeft;
    [SerializeField] private float skillCooldownLeft;

    private void Start()
    {
        if (actionsVar != null)
        {
            actionsVar.Provide().Gameplay.Attack.performed += OnTryAttack;
            actionsVar.Provide().Gameplay.Special.performed += OnTrySpecial;
        }
    }

    private void Update()
    {
        cooldownLeft -= Time.deltaTime;
        skillCooldownLeft -= Time.deltaTime;

        if (actionsVar != null)
        {
            var input = actionsVar.Provide().Gameplay.Move.ReadValue<Vector2>();
            if (input != Vector2.zero) rotation = Vector2.SignedAngle(Vector2.right, input);
        }
    }

    private void OnTryAttack(InputAction.CallbackContext context)
    {
        if (cooldownLeft <= 0)
        {
            if (entity != null) basicAttack.Instantiate(entity, transform.position, rotation);
            cooldownLeft = cooldown;
        }
    }

    private void OnTrySpecial(InputAction.CallbackContext context)
    {
        if (skillCooldownLeft <= 0)
        {
            if (entity != null) specialAttack.Instantiate(entity, transform.position, rotation);
            skillCooldownLeft = skillCooldown;
        }
    }
}
