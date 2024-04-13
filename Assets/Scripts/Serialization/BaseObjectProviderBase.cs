#nullable enable
using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class BaseObjectProviderBase
{
    public abstract SaveableObject GetBaseObject(SaveablePrefabList prefabList);
    
    public virtual BaseObjectProviderBase GetLatest()
    {
        return this;
    }
}
