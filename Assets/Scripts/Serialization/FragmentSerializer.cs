#nullable enable
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class FragmentSerializer : MonoBehaviour, ISerializationId
{
    [field: SerializeField]
    public string Id { get; private set; } = Guid.NewGuid().ToString();

    public FragmentSaveObject Save()
    {
        var ret = new FragmentSaveObject(Id);
        var adapters = GetComponents<SerializationFragmentSource>()!;
        foreach (var adapter in adapters)
        {
            ret.Add(adapter.Generate());
        }
        return ret;
    }

    public void Load(SerializationDatabase database, FragmentSaveObject saveObject)
    {
        Id = saveObject.Id;
        GameObject? obj = null;
        foreach (var fragment in saveObject.Fragments.Values)
        {
            obj = fragment.Apply(database, obj);
        }
    }
    
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, IsReference = true)]
    public class FragmentSaveObject
    {
        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("fragments")]
        public SortedList<int, SerializationFragment> Fragments { get; } = new();

        public FragmentSaveObject(string id)
        {
            Id = id;
        }

        public void Add(SerializationFragment fragment)
        {
            Fragments.Add(fragment.Order, fragment);
        }
    }
}
