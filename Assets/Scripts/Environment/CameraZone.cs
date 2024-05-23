#nullable enable
using UnityEngine;

public class CameraZone : Zone
{
    public float innerRadius;

    public float outerRadius;

    public float GetWeight(Vector3 position)
    {
        var dist = (transform.position - position).magnitude;
        if (dist <= innerRadius) return 1;
        if (dist >= outerRadius) return 0;
        return 1 - (dist - innerRadius) / (outerRadius - innerRadius);
    }
}
