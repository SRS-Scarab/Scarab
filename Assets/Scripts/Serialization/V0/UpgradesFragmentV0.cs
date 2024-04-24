#nullable enable
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class UpgradesFragmentV0 : SaveFragmentBase
{
    [JsonProperty("upgrades")]
    public List<string> Upgrades { get; }
    
    [JsonConstructor]
    public UpgradesFragmentV0(List<string> upgrades, string id) : base(id)
    {
        Upgrades = upgrades;
    }

    public override SaveFragmentBase GetLatest()
    {
        return this;
    }
}
