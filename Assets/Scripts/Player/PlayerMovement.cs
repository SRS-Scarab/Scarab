using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public ActionsVariable actionsVar;
    public Rigidbody2D rb;
    public float speed;
    public float sprintSpeed;

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
        UpdateMovement(speed);

        if (Input.GetKey(KeyCode.LeftShift)) {
            Sprint();
        }
    }
    private void UpdateMovement(float speed)
    {
        rb.velocity = moveAction.ReadValue<Vector2>().normalized * speed;
    }

    private void Sprint() 
    {
        UpdateMovement(sprintSpeed);
    }
}
