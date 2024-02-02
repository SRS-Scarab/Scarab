using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public ActionsVariable actionsVar;
    public Rigidbody2D rb;
    public float speed;

    private InputAction moveAction;
    void Start()
    {
        Physics2D.gravity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        speed = 5f;
        moveAction = actionsVar.Provide().Gameplay.Move;
    }

    void Update()
    {
        UpdateMovement();
    }
    private void UpdateMovement()
    {
        rb.velocity = moveAction.ReadValue<Vector2>().normalized * speed;
    }
}
