#nullable enable
using System.Linq;
using UnityEngine;

public class PlayerValues : Blackboard
{
    public Vector3 moveVelocity;
    
    [SerializeField]
    private ZoneChecker? zoneChecker;
    
    [SerializeField]
    private float cameraAngle;
    
    [SerializeField]
    private float cameraElevation = 15;
    
    [SerializeField]
    private float cameraDistance = 10;
    
    [SerializeField]
    private float speedMultiplier = 1;

    public float GetCameraAngle()
    {
        if (zoneChecker == null) return ToSafeCameraAngle(cameraAngle);

        var zones = zoneChecker.GetZones<CameraAngleZone>().ToArray();
        if (!zones.Any()) return ToSafeCameraAngle(cameraAngle);

        var angles = zones.Select(e => e.cameraAngle).ToList();
        var weights = zones.Select(e => e.GetWeight(transform.position)).ToList();

        if (weights.Sum() < 1)
        {
            angles.Add(cameraAngle);
            weights.Add(1 - weights.Sum());
        }
        
        return ToSafeCameraAngle(MathUtils.WeightedAverage(angles, weights));
    }

    private float ToSafeCameraAngle(float angle)
    {
        return MathUtils.Mod(angle, 360);
    }

    public float GetCameraElevation()
    {
        if (zoneChecker == null) return ToSafeCameraElevation(cameraElevation);

        var zones = zoneChecker.GetZones<CameraElevationZone>().ToArray();
        if (!zones.Any()) return ToSafeCameraElevation(cameraElevation);

        var elevations = zones.Select(e => e.cameraElevation).ToList();
        var weights = zones.Select(e => e.GetWeight(transform.position)).ToList();

        if (weights.Sum() < 1)
        {
            elevations.Add(cameraElevation);
            weights.Add(1 - weights.Sum());
        }
        
        return ToSafeCameraElevation(MathUtils.WeightedAverage(elevations, weights));
    }

    private float ToSafeCameraElevation(float elevation)
    {
        return Mathf.Clamp(elevation, 0, 85);
    }

    public float GetCameraDistance()
    {
        if (zoneChecker == null) return ToSafeCameraDistance(cameraDistance);

        var zones = zoneChecker.GetZones<CameraDistanceZone>().ToArray();
        if (!zones.Any()) return ToSafeCameraDistance(cameraDistance);

        var distances = zones.Select(e => e.cameraDistance).ToList();
        var weights = zones.Select(e => e.GetWeight(transform.position)).ToList();

        if (weights.Sum() < 1)
        {
            distances.Add(cameraDistance);
            weights.Add(1 - weights.Sum());
        }
        
        return ToSafeCameraDistance(MathUtils.WeightedAverage(distances, weights));
    }

    private float ToSafeCameraDistance(float distance)
    {
        return Mathf.Max(distance, 2);
    }

    public float GetSpeedMultiplier()
    {
        if (zoneChecker == null) return speedMultiplier;

        var zone = zoneChecker.GetZones<SpeedZone>().LastOrDefault();
        return zone == null ? speedMultiplier : zone.speedMultiplier;
    }
}
