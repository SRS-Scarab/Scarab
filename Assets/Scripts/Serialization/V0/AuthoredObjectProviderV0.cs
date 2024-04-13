#nullable enable
using System;
using System.Linq;
using Newtonsoft.Json;
using Object = UnityEngine.Object;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class AuthoredObjectProviderV0 : BaseObjectProviderBase
{
    [JsonProperty("id")]
    public string Id { get; }

    public AuthoredObjectProviderV0(string id)
    {
        Id = id;
    }
    
    public override SaveableObject GetBaseObject(SaveablePrefabList prefabList)
    {
        return Object.FindObjectsOfType<SaveableObject>().First(e => e is SaveableAuthoredObject authored && authored.GetId() == Id);
    }
}
