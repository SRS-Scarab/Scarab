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
        float xdir = moveAction.ReadValue<Vector2>().x;
        float ydir = moveAction.ReadValue<Vector2>().y;
        rb.velocity = new Vector2(xdir, ydir).normalized * speed;
        // only rotate if moving
        if (xdir != 0 || ydir != 0)
        {
            float rot_z = Mathf.Atan2(ydir, xdir) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
    }
}
