#nullable enable
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn, IsReference = true)]
public interface ISerializationId
{
    [JsonProperty("id")]
    public string Id { get; }
}
