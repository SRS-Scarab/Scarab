#nullable enable
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Map Colors")]
public class MapColors : ScriptableObject
{
    public MapColorEntry[] entries = Array.Empty<MapColorEntry>();
}

[Serializable]
public struct MapColorEntry
{
    public Sprite sprite;
    public Color color;
}
