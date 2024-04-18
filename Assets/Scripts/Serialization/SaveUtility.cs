#nullable enable
using System.Linq;
using UnityEngine;

public static class SaveUtility
{
    public static SaveDataBase Save(string name)
    {
        var data = new SaveDataV0(name);
        data.Objects.AddRange(Object.FindObjectsOfType<SaveableObject>().Select(e => e.Save()));
        return data;
    }

    public static void Load(SaveDataBase data, SaveablePrefabList prefabList)
    {
        var latest = (SaveDataV0)data.GetLatest();
        foreach (var obj in latest.Objects)
        {
            obj.LoadToBase(prefabList);
        }
    }

    public static void Clear()
    {
        Object.FindObjectsOfType<SaveablePrefabInstance>().ToList().ForEach(e => Object.Destroy(e.gameObject));
    }
}
