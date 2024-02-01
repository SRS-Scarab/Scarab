#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Dependencies")]

    [SerializeField] private ActionsVariable? actionsVar;

    [SerializeField] private CombatEntity? entity;
    [SerializeField] private Rigidbody2D? rb;
    [SerializeField] private Rigidbody2D? attackArea;

    [Header("Parameters")]

    [SerializeField] private AttackInfo basicAttack;
    [SerializeField] private AttackInfo specialAttack;
    [SerializeField] private float cooldown;
    [SerializeField] private float skillCooldown;

    [Header("State")]

    [SerializeField] private bool isFacingLeft;
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
        if (rb != null && rb.velocity.x != 0) isFacingLeft = rb.velocity.x < 0; // todo change to interface directly with movement script later
    }

    private void OnTryAttack(InputAction.CallbackContext context)
    {
        if (cooldownLeft <= 0)
        {
            if (entity != null && attackArea != null)
            {
                basicAttack.Instantiate(entity, new Vector3(attackArea.position.x, attackArea.position.y, 0), attackArea.rotation);
            }
            cooldownLeft = cooldown;
        }
    }

    private void OnTrySpecial(InputAction.CallbackContext context)
    {
        if (skillCooldownLeft <= 0)
        {
            if (entity != null && attackArea != null)
            {
                specialAttack.Instantiate(entity, new Vector3(attackArea.position.x, attackArea.position.y, 0), attackArea.rotation);
            }
            skillCooldownLeft = skillCooldown;
        }
    }
}
