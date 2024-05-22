#nullable enable
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    private Rigidbody? parentRigidbody;

    public bool IsGrounded()
    {
        return isGrounded;
    }

    private void Update()
    {
        var t = transform;
        var colliders = Physics.OverlapSphere(t.position, 0.1f);
        isGrounded = false;
        foreach (var collider in colliders)
        {
            if (collider.attachedRigidbody != parentRigidbody)
            {
                isGrounded = true;
                break;
            }
        }
    }
}