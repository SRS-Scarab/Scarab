#nullable enable
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Serialization/Prefab")]
public class SaveablePrefab : ScriptableObject
{
    public string id = Guid.NewGuid().ToString();
    public GameObject prefab = null!;
}
