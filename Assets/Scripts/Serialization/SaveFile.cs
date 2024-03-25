#nullable enable
using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class SaveFile
{
    [JsonProperty("metadata")]
    public SaveMetadata Metadata { get; }

    [JsonProperty("data")]
    public SaveData Data { get; }

    public SaveFile(SaveMetadata metadata, SaveData data)
    {
        Metadata = metadata;
        Data = data;
    }
}
