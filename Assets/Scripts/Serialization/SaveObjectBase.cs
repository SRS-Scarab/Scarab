#nullable enable
using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class SaveObjectBase
{
    public abstract void LoadToBase(SaveablePrefabList prefabList);
    
    public virtual SaveObjectBase GetLatest()
    {
        return this;
    }
}
