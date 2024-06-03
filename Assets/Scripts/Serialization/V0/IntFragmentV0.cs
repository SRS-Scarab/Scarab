#nullable enable
using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class IntFragmentV0 : SaveFragmentBase
{
    [JsonProperty("value")]
    public int Value { get; }
    
    [JsonConstructor]
    public IntFragmentV0(int value, string id) : base(id)
    {
        Value = value;
    }
    
    public override SaveFragmentBase GetLatest()
    {
        return this;
    }
}
