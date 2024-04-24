#nullable enable
using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class PlayerFragmentV0 : SaveFragmentBase
{
    [JsonProperty("action-cooldown")]
    public float ActionCooldown { get; }
    
    [JsonProperty("special-cooldown")]
    public float SpecialCooldown { get; }
    
    [JsonProperty("selected-index")]
    public float SelectedIndex { get; }
    
    [JsonConstructor]
    public PlayerFragmentV0(float actionCooldown, float specialCooldown, float selectedIndex, string id) : base(id)
    {
        ActionCooldown = actionCooldown;
        SpecialCooldown = specialCooldown;
        SelectedIndex = selectedIndex;
    }

    public override SaveFragmentBase GetLatest()
    {
        return this;
    }
}