#nullable enable
using System;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class SaveData
{
    protected SerializationDatabase Database { get; }

    protected SaveData(SerializationDatabase database)
    {
        Database = database;
    }
}
