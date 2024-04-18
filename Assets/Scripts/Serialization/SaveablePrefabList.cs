#nullable enable
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Serialization/Prefab List")]
public class SaveablePrefabList : ScriptableVariable<List<SaveablePrefab>>
{
    [SerializeField]
    private List<SaveablePrefab> prefabs = new();
    
    public override List<SaveablePrefab> Provide()
    {
        return prefabs;
    }

    public override void Consume(List<SaveablePrefab> value)
    {
        prefabs = value;
    }
}
