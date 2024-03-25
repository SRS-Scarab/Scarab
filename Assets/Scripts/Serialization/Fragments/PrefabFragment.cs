#nullable enable
using System;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class PrefabFragment : CreationalFragment
{
    [JsonProperty("prefab-id")]
    public string PrefabId { get; }

    public PrefabFragment(string prefabId)
    {
        PrefabId = prefabId;
    }

    public override GameObject Apply(SerializationDatabase database, GameObject? obj)
    {
        return Object.Instantiate(database.Get<SerializationPrefab>(PrefabId)!.Prefab);
    }
}
