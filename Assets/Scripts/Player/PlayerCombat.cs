#nullable enable
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private CombatEntity? entity;
    [SerializeField] private Rigidbody2D? rb;
    
    [Header("Parameters")]
    
    [SerializeField] private AttackInfo basicAttack;
    [SerializeField] private float cooldown;
    
    [Header("State")]
    
    [SerializeField] private bool isFacingLeft;
    [SerializeField] private float cooldownLeft;
    
    private void Update()
    {
        cooldownLeft -= Time.deltaTime;
        if (rb != null && rb.velocity.x != 0) isFacingLeft = rb.velocity.x < 0;
        if (Input.GetMouseButtonDown(0) && cooldownLeft <= 0)
        {
            if (entity != null)
            {
                basicAttack.Instantiate(entity, transform.position, isFacingLeft ? 180 : 0);
            }
            cooldownLeft = cooldown;
        }
    }
}
