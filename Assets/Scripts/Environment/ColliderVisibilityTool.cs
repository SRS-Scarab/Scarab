#nullable enable
using UnityEngine;

[ExecuteInEditMode]
public class ColliderVisibilityTool : MonoBehaviour
{
    [SerializeField]
    private bool enable;

    private void Update()
    {
        if (enable)
        {
            enable = false;

            Execute(gameObject);
        }
    }

    private void Execute(GameObject obj)
    {
        var renderer = obj.GetComponent<Renderer>();
        var collider = obj.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = renderer != null && renderer.enabled;
        }

        foreach (Transform child in obj.transform)
        {
            Execute(child.gameObject);
        }
    }
}
