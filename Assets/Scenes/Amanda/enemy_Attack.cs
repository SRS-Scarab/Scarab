using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Attack : MonoBehaviour
{
    public float speed = 3f;
    public CombatEntity entity;
    public AttackInfo attackInfo;
    public float attackSpeed = 1f;
    private float canAttack;
    private Transform target;

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
        if (other.gameObject.CompareTag("Player"))
        {
            if (attackSpeed <= canAttack)
            {
                attackInfo.TryAttack(entity, transform.position, Vector2.Angle(Vector2.right, target.position - transform.position));
                canAttack = 0f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = null;
        }
    }


}
