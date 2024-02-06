#nullable enable
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Variables/Tilemap")]
public class TilemapVariable : ScriptableVariable<Tilemap?>
{
    [SerializeField] private Tilemap? tilemap;

    public override Tilemap? Provide() => tilemap;

    public override void Consume(Tilemap? value) => tilemap = value;
}
