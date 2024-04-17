#nullable enable
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class SaveDataV0 : SaveDataBase
{
    [JsonProperty("name")]
    public string Name { get; }
    
    [JsonProperty("objects")]
    public List<SaveObjectBase> Objects { get; } = new();

    [JsonConstructor]
    public SaveDataV0(string name)
    {
        Name = name;
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
