using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerHP playerHP;
    void Start()
    {
        Physics2D.gravity = Vector2.zero;
    }

    void Update()
    {
        playerHP.damage(1);
    }
}
