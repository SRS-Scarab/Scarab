#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private ActionsVariable? actionsVar;
    [SerializeField] private CombatEntity? entity;
    [SerializeField] private Rigidbody2D? rb;
    
    [Header("Parameters")]
    
    [SerializeField] private AttackInfo basicAttack;
    [SerializeField] private float cooldown;
    
    [Header("State")]
    
    [SerializeField] private bool isFacingLeft;
    [SerializeField] private float cooldownLeft;

    private void Start()
    {
        if (actionsVar != null) actionsVar.Provide().Gameplay.Attack.performed += OnTryAttack;
    }

    private void Update()
    {
        cooldownLeft -= Time.deltaTime;
        if (rb != null && rb.velocity.x != 0) isFacingLeft = rb.velocity.x < 0; // todo change to interface directly with movement script later
    }

    private void OnTryAttack(InputAction.CallbackContext context)
    {
        if (cooldownLeft <= 0)
        {
            if (entity != null) basicAttack.Instantiate(entity, transform.position, isFacingLeft ? 180 : 0);
            cooldownLeft = cooldown;
        }
    }
}
