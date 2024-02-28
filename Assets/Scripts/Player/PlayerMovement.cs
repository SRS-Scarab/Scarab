using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public ActionsVariable actionsVar;
    public Rigidbody2D rb;
    public float speed;
    public float sprintSpeed;

    [SerializeField] private bool isSprinting;
    private InputAction _moveAction;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        _moveAction = actionsVar.Provide().Gameplay.Move;
        actionsVar.Provide().Gameplay.Sprint.performed += OnBeginSprint;
        actionsVar.Provide().Gameplay.Sprint.canceled += OnEndSprint;
    }

    void Update()
    {
        UpdateMovement(isSprinting ? sprintSpeed : speed);
    }
    
    private void UpdateMovement(float inSpeed)
    {
        rb.velocity = _moveAction.ReadValue<Vector2>().normalized * inSpeed;
    }

    private void OnBeginSprint(InputAction.CallbackContext context)
    {
        isSprinting = true;
    }

    private void OnEndSprint(InputAction.CallbackContext context)
    {
        isSprinting = false;
    }
}
