#nullable enable
using System.Linq;
using UnityEngine;

public class PlayerValues : Blackboard
{
    [SerializeField]
    private ZoneChecker? zoneChecker;
    
    [SerializeField]
    private float cameraDirection;
    
    [SerializeField]
    private float speedMultiplier = 1;

    public float GetCameraDirection()
    {
        if (zoneChecker == null) return cameraDirection;

        var zones = zoneChecker.GetZones<CameraZone>().ToArray();
        if (!zones.Any()) return MathUtils.Mod(cameraDirection, 360);

        var dirs = zones.Select(e => e.cameraDirection).ToList();
        var weights = zones.Select(e => e.GetWeight(transform.position)).ToList();

        if (weights.Sum() < 1)
        {
            dirs.Add(cameraDirection);
            weights.Add(1 - weights.Sum());
        }
        
        return MathUtils.Mod(MathUtils.WeightedAverage(dirs, weights), 360);
    }

    public float GetSpeedMultiplier()
    {
        if (zoneChecker == null) return speedMultiplier;

        var zone = zoneChecker.GetZones<SpeedZone>().LastOrDefault();
        return zone == null ? speedMultiplier : zone.speedMultiplier;
    }
}
