#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Generation/Types/Structure")]
public class GeneratedStructureType : ScriptableObject
{
    public GameObject? structurePrefab;
    
    public Tilemap? GetPrefabTilemap()
    {
        return structurePrefab == null ? null : structurePrefab.GetComponentInChildren<Tilemap>();
    }

    public IEnumerable<GeneratedConnection> GetPrefabConnections()
    {
        return structurePrefab == null ? Array.Empty<GeneratedConnection>() : structurePrefab.GetComponents<GeneratedConnection>();
    }

    public IEnumerable<GeneratedConnection> GetCompatibleConnections(GeneratedConnectionType? type)
    {
        return GetPrefabConnections().Where(connection => connection.connectionType != null && connection.connectionType.IsCompatible(type)).ToList();
    }
}