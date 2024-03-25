#nullable enable
using System;
using UnityEngine;

public class SerializationPrefab : ScriptableObject, ISerializationId
{
    [field: SerializeField]
    public string Id { get; private set; } = Guid.NewGuid().ToString();

    [field: SerializeField]
    public GameObject Prefab { get; private set; } = null!;
}
