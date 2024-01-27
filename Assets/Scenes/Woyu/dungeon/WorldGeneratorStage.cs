#nullable enable
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Generation/Stages/World")]
public class WorldGeneratorStage : GeneratorStage
{
    public TileBase? baseTile;
    public float featureProbability;
    public TileBase[] featureTiles = Array.Empty<TileBase>();
    public int generationBounds;
    
    public override void Generate(Tilemap target)
    {
        for (var x = -generationBounds; x <= generationBounds; x++)
        {
            for (var y = -generationBounds; y <= generationBounds; y++)
            {
                var generated = Random.Range(0.0f, 1);
                if (generated <= featureProbability)
                {
                    var feature = featureTiles[Random.Range(0, featureTiles.Length)];
                    target.SetTile(new Vector3Int(x, y, 0), feature);
                }
                else
                {
                    target.SetTile(new Vector3Int(x, y, 0), baseTile);
                }
            }
        }
    }
}
