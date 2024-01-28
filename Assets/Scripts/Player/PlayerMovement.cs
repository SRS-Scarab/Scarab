using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerHP playerHP;
    public Rigidbody rb;
    void Start()
    {
        Physics2D.gravity = Vector2.zero;
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        UpdateMovement();
    }
    private void UpdateMovement()
    {
        List<string> buttonNames = new List<string>() { "Left", "Right", "Up", "Down" };
        if (Input.GetButton("Left"))
            rb.velocity = Vector3Int.left;
        else if (Input.GetButton("Right"))
            rb.velocity = Vector3Int.right;
        else if (Input.GetButton("Up"))
            rb.velocity = Vector3Int.up;
        else if (Input.GetButton("Down"))
            rb.velocity = Vector3Int.down;
    }
}
