#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Generation/Stages/Structure")]
public class StructureGeneratorStage : GeneratorStage
{
    public GeneratedStructureType? seedStructure;
    public GeneratedStructureType[] allowedTypes = Array.Empty<GeneratedStructureType>();
    public int generationBounds;

    public override void Generate(Tilemap target)
    {
        var pending = new List<GeneratedConnectionMetadata>();
        var overlapped = new HashSet<Vector2Int>();

        GenerateStructure(target, seedStructure, Vector2Int.zero, pending);

        while (pending.Count > 0)
        {
            var metadata = pending[0];
            pending.RemoveAt(0);

            if (!overlapped.Contains(metadata.WorldPosition) && Mathf.Abs(metadata.WorldPosition.x) <= generationBounds && Mathf.Abs(metadata.WorldPosition.y) <= generationBounds)
            {
                overlapped.Add(metadata.WorldPosition);
                var compatibleTypes = allowedTypes.Where(x => x.GetCompatibleConnections(metadata.ConnectionType).Any()).ToList();
                var type = compatibleTypes[Random.Range(0, compatibleTypes.Count)];
                var compatibleConnections = type.GetCompatibleConnections(metadata.ConnectionType).ToList();
                var connection = compatibleConnections[Random.Range(0, compatibleConnections.Count)];
                var position = metadata.WorldPosition - connection.tilePosition;
                GenerateStructure(target, type, position, pending);
            }
        }
    }

    private void GenerateStructure(Tilemap target, GeneratedStructureType? type, Vector2Int position, List<GeneratedConnectionMetadata> pending)
    {
        if (type == null) return;
        var from = type.GetPrefabTilemap();
        if (from == null) return;
        var bounds = from.cellBounds;
        for (var x = bounds.xMin; x <= bounds.xMax; x++)
        {
            for (var y = bounds.yMin; y <= bounds.yMax; y++)
            {
                for (var z = bounds.zMin; z <= bounds.zMax; z++)
                {
                    var tile = from.GetTile(new Vector3Int(x, y, z));
                    if (tile != null)
                    {
                        target.SetTile(new Vector3Int(x + position.x, y + position.y, z), tile);
                    }
                }
            }
        }

        pending.AddRange(type.GetPrefabConnections().Select(x => new GeneratedConnectionMetadata(x.tilePosition + position, x.connectionType)));
    }

    private record GeneratedConnectionMetadata(Vector2Int WorldPosition, GeneratedConnectionType? ConnectionType);
}