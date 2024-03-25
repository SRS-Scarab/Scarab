#nullable enable
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Serialization Database")]
public class SerializationDatabase : ScriptableObject
{
    private readonly Dictionary<string, ISerializationId> _database = new();

    public void Clear()
    {
        _database.Clear();
    }
    
    public void Register(ISerializationId obj)
    {
        _database[obj.Id] = obj;
    }

    public T? Get<T>(string id) where T : ISerializationId
    {
        _database.TryGetValue(id, out var ret);
        return (T?)ret;
    }
}
