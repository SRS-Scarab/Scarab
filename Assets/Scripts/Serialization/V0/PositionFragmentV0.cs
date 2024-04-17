#nullable enable
using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class PositionFragmentV0 : SaveFragmentBase
{
    [JsonProperty("x")]
    public float X { get; }
    
    [JsonProperty("y")]
    public float Y { get; }
    
    [JsonProperty("z")]
    public float Z { get; }
    
    [JsonConstructor]
    public PositionFragmentV0(float x, float y, float z, string id) : base(id)
    {
        X = x;
        Y = y;
        Z = z;
    }
    
    public override SaveFragmentBase GetLatest()
    {
        return this;
    }
}
