#nullable enable
using System;
using UnityEngine;

public class SaveableAuthoredObject : SaveableObject
{
    [SerializeField]
    private string id = Guid.NewGuid().ToString();

    public string GetId()
    {
        return id;
    }
    
    public override SaveObjectBase Save()
    {
        var obj = new SaveObjectV0(new AuthoredObjectProviderV0(id));
        obj.Fragments.AddRange(SaveFragments());
        return obj;
    }

    public override void Load(SaveObjectBase obj)
    {
        var latest = (SaveObjectV0)obj.GetLatest();
        LoadFragments(latest.Fragments);
    }
}
