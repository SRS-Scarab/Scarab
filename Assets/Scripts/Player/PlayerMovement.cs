using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerHP playerHP;
    public Rigidbody2D rb;
    public float speed;
    void Start()
    {
        Physics2D.gravity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        speed = 5f;
    }

    void Update()
    {
        UpdateMovement();
    }
    private void UpdateMovement()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
    }
}
