using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
  public ActionsVariable actionsVar;
  public Rigidbody rb;
  public float speed;
  public float sprintSpeed;

  [SerializeField] private bool isSprinting;
  private InputAction _moveAction;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
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
    Vector2 actions = _moveAction.ReadValue<Vector2>();
    Vector3 thing = Quaternion.AngleAxis(-45, Vector3.up) * new Vector3(actions.x, 0, actions.y);
    Vector3 downward = Vector3.up * rb.velocity.y;
    rb.velocity = thing.normalized * inSpeed + downward;
    // rb.velocity = _moveAction.ReadValue<Vector2>().normalized * inSpeed;
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
