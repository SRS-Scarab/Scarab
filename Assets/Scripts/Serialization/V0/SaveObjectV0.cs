#nullable enable
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class SaveObjectV0 : SaveObjectBase
{
    [JsonProperty("base-object")]
    public BaseObjectProviderBase BaseObject { get; private set; }
    
    [JsonProperty("fragments")]
    public List<SaveFragmentBase> Fragments { get; } = new();

    public SaveObjectV0(BaseObjectProviderBase baseObject)
    {
        BaseObject = baseObject;
    }

    public override void LoadToBase(SaveablePrefabList prefabList)
    {
        BaseObject.GetLatest().GetBaseObject(prefabList).Load(this);
    }

    public override SaveObjectBase GetLatest()
    {
        BaseObject = BaseObject.GetLatest();
        
        for (var i = 0; i < Fragments.Count; i++)
        {
            Fragments[i] = Fragments[i].GetLatest();
        }
        
        return this;
    }
}
