#nullable enable
using UnityEngine;

public class SaveablePrefabInstance : SaveableObject
{
    [SerializeField]
    private SaveablePrefab prefab = null!;

    public SaveablePrefab GetPrefab()
    {
        return prefab;
    }
    
    public override SaveObjectBase Save()
    {
        var obj = new SaveObjectV0(new PrefabInstanceProviderV0(prefab));
        obj.Fragments.AddRange(SaveFragments());
        return obj;
    }

    public override void Load(SaveObjectBase obj)
    {
        var latest = (SaveObjectV0)obj.GetLatest();
        LoadFragments(latest.Fragments);
    }
}
