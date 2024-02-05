#nullable enable
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Map Tile")]
public class MapTile : Tile
{
    [SerializeField] private Color mapColor = Color.clear;

    public Color GetMapColor() => mapColor;
}
