#nullable enable
using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class SaveDataBase
{
    public virtual SaveDataBase GetLatest()
    {
        return this;
    }
}
