using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Attack : MonoBehaviour
{
    public float speed = 3f;
    public float attackDamage = 10f;
    public float attackSpeed = 1f;
    private float canAttack;
    private Transform target;
    [SerializeField] private float knockback = 5f;
    [SerializeField] private PlayerHP playerHP;

    private void Update()
    {
        canAttack += Time.deltaTime;
        if (target != null && attackSpeed <= canAttack)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                playerHP.damage(attackDamage);
                Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
                // don't know why this doesn't work properly with impulse
                if (rb != null) rb.AddForce((other.transform.position - transform.position).normalized * knockback * 100, ForceMode2D.Force);
                canAttack = 0f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }


}
