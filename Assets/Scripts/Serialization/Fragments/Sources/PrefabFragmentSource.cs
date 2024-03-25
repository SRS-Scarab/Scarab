#nullable enable
using UnityEngine;

public class PrefabFragmentSource : SerializationFragmentSource
{
    [field: SerializeField]
    public SerializationPrefab Prefab { get; private set; } = null!;
    
    public override SerializationFragment Generate()
    {
        return new PrefabFragment(Prefab.Id);
    }
}