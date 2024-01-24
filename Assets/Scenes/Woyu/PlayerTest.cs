using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Parameters")]
    [SerializeField] private float runSpeed = 5;
    [SerializeField] private float runWindup = 0.05f;
    [SerializeField] private float jumpForce = 7.5f;
    [SerializeField] private float softJumpMultiplier = 0.7f;
    [SerializeField] private int maxDashes = 1;
    [SerializeField] private float dashForce = 25;
    
    [Header("State")]
    [SerializeField] private float runMomentum = 0;
    [SerializeField] private float facing = 1;
    [SerializeField] private int groundContact = 0;
    [SerializeField] private int softContact = 0;
    [SerializeField] private int dashCharges = 0;

    private void Update()
    {
        if (groundContact > 0)
        {
            dashCharges = maxDashes;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    private void FixedUpdate()
    {
        Move(Input.GetAxis("Horizontal"));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            groundContact++;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            groundContact--;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            softContact++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            softContact--;
        }
    }

    private void Move(float amount)
    {
        if (amount != 0) facing = Mathf.Sign(amount);
        runMomentum = Lerp(rb.velocity.x, amount * runSpeed, (runSpeed / runWindup) * Time.fixedDeltaTime);
        rb.velocity = new Vector2(runMomentum, rb.velocity.y);
    }

    private void Jump()
    {
        if (groundContact > 0)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else if (softContact > 0)
        {
            rb.AddForce(Vector2.up * (jumpForce * softJumpMultiplier), ForceMode2D.Impulse);
        }
    }

    private void Dash()
    {
        if (dashCharges > 0)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.right * (facing * dashForce), ForceMode2D.Impulse);
            dashCharges--;
        }
    }

    private float Lerp(float from, float to, float delta)
    {
        float diff = to - from;
        if (Mathf.Abs(diff) <= delta) return to;
        return from + delta * Mathf.Sign(diff);
    }
}