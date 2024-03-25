#nullable enable
using Newtonsoft.Json;
using UnityEngine;

public class TransformFragment : SerializationFragment
{
    [JsonProperty("position")]
    public float[] Position { get; }
    
    [JsonProperty("rotation")]
    public float[] Rotation { get; }

    public TransformFragment(Transform transform)
    {
        var position = transform.position;
        Position = new[]
        {
            position.x,
            position.y,
            position.z
        };
        var eulerAngles = transform.eulerAngles;
        Rotation = new[]
        {
            eulerAngles.x,
            eulerAngles.y,
            eulerAngles.z
        };
    }
    
    public override GameObject Apply(SerializationDatabase database, GameObject? obj)
    {
        var transform = obj!.transform;
        transform.position = new Vector3(Position[0], Position[1], Position[2]);
        transform.eulerAngles = new Vector3(Rotation[0], Rotation[1], Rotation[2]);
        return obj;
    }
}
