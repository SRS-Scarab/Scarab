#nullable enable
using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class SaveFragmentBase
{
    [JsonProperty("id")]
    public string Id { get; }
    
    [JsonConstructor]
    protected SaveFragmentBase(string id)
    {
        Id = id;
    }
    
    public virtual SaveFragmentBase GetLatest()
    {
        return this;
    }
}
