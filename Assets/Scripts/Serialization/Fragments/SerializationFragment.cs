#nullable enable
using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class SerializationFragment
{
    [JsonProperty("order")]
    public int Order { get; }

    protected SerializationFragment(int order = 0)
    {
        Order = order;
    }

    public abstract GameObject Apply(SerializationDatabase database, GameObject? obj);
}
