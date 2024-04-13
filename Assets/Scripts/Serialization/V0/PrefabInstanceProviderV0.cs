#nullable enable
using System;
using System.Linq;
using Newtonsoft.Json;
using Object = UnityEngine.Object;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class PrefabInstanceProviderV0 : BaseObjectProviderBase
{
    [JsonProperty("prefab-id")]
    public string PrefabId { get; }

    public PrefabInstanceProviderV0(SaveablePrefab prefab)
    {
        PrefabId = prefab.id;
    }
    
    public override SaveableObject GetBaseObject(SaveablePrefabList prefabList)
    {
        return Object.Instantiate(prefabList.Provide().First(e => e.id == PrefabId).prefab).GetComponent<SaveableObject>();
    }
}
