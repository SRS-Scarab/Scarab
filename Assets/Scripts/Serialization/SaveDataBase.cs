#nullable enable
using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class SaveDataBase
{
    [JsonProperty("name")]
    public string Name { get; }
    
    protected SaveDataBase(string name = "")
    {
        Name = name;
    }

    public virtual SaveDataBase GetLatest()
    {
        return this;
    }
}
