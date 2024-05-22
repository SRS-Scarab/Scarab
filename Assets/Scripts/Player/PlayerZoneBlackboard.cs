#nullable enable
using System.Linq;
using UnityEngine;

public class PlayerZoneBlackboard : MonoStateMachineBlackboard
{
    [SerializeField]
    private ZoneChecker? zoneChecker;
    
    [SerializeField]
    private Vector3 cameraDirection = Vector3.forward;
    
    [SerializeField]
    private float speedMultiplier = 1;

    public Vector3 GetCameraDirection()
    {
        if (zoneChecker == null) return cameraDirection;

        var zone = (CameraZone?)zoneChecker.GetZones().LastOrDefault(e => e is CameraZone);
        return zone == null ? cameraDirection : zone.cameraDirection;
    }

    public float GetSpeedMultiplier()
    {
        if (zoneChecker == null) return speedMultiplier;

        var zone = (SpeedZone?)zoneChecker.GetZones().LastOrDefault(e => e is SpeedZone);
        return zone == null ? speedMultiplier : zone.speedMultiplier;
    }
}
