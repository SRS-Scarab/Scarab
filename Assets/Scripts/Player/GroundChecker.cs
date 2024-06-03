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
        var colliders = Physics.OverlapBox(t.position, new Vector3(0.5f, 0.1f, 0.5f));
        isGrounded = false;
        foreach (var collider in colliders)
        {
            if (!collider.isTrigger && collider.attachedRigidbody != parentRigidbody)
            {
                isGrounded = true;
                break;
            }
        }
    }
}