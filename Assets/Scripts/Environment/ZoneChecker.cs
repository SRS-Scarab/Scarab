#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZoneChecker : MonoBehaviour
{
    [SerializeField]
    private List<Zone> zones = new();

    public IEnumerable<Zone> GetZones()
    {
        return zones;
    }

    public IEnumerable<T> GetZones<T>()
    {
        return zones.Where(e => e is T).Cast<T>();
    }

    private void Update()
    {
        foreach (var zone in zones)
        {
            zone.OnTick(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var newZones = other.GetComponents<Zone>();
        foreach (var zone in newZones)
        {
            zone.OnEnter(this);
            zones.Add(zone);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var newZones = other.GetComponents<Zone>();
        foreach (var zone in newZones)
        {
            zone.OnExit(this);
            zones.Remove(zone);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var newZones = other.GetComponents<Zone>();
        foreach (var zone in newZones)
        {
            zone.OnEnter(this);
            zones.Add(zone);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var newZones = other.GetComponents<Zone>();
        foreach (var zone in newZones)
        {
            zone.OnExit(this);
            zones.Remove(zone);
        }
    }
}
