#nullable enable
using System.Collections.Generic;
using UnityEngine;

public class ZoneChecker : MonoBehaviour
{
    [SerializeField]
    private List<Zone> zones = new();

    public Zone[] GetZones()
    {
        return zones.ToArray();
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
        var zone = other.GetComponent<Zone>();
        if (zone != null)
        {
            zone.OnEnter(this);
            zones.Add(zone);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var zone = other.GetComponent<Zone>();
        if (zone != null)
        {
            zone.OnExit(this);
            zones.Remove(zone);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var zone = other.GetComponent<Zone>();
        if (zone != null)
        {
            zone.OnEnter(this);
            zones.Add(zone);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var zone = other.GetComponent<Zone>();
        if (zone != null)
        {
            zone.OnExit(this);
            zones.Remove(zone);
        }
    }
}
