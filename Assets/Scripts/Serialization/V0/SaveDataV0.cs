#nullable enable
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class SaveDataV0 : SaveDataBase
{
    [JsonProperty("objects")]
    public List<SaveObjectBase> Objects { get; } = new();

    public SaveDataV0(string name = "") : base(name)
    {
    }

    public override SaveDataBase GetLatest()
    {
        for (var i = 0; i < Objects.Count; i++)
        {
            Objects[i] = Objects[i].GetLatest();
        }
        
        return this;
    }
}
