#nullable enable
using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class SaveMetadata
{
    [JsonProperty("version")]
    public string Version { get; }
    
    [JsonProperty("name")]
    public string Name { get; }

    public SaveMetadata(string version = "", string name = "")
    {
        Version = version;
        Name = name;
    }
}
