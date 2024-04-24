#nullable enable
using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class CombatEntityFragmentV0 : SaveFragmentBase
{
    [JsonProperty("health")]
    public float Health { get; }
    
    [JsonProperty("mana")]
    public float Mana { get; }
    
    [JsonConstructor]
    public CombatEntityFragmentV0(float health, float mana, string id) : base(id)
    {
        Health = health;
        Mana = mana;
    }

    public override SaveFragmentBase GetLatest()
    {
        return this;
    }
}
