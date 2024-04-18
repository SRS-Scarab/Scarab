#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SaveableObject : MonoBehaviour
{
    public abstract SaveObjectBase Save();

    public abstract void Load(SaveObjectBase obj);

    protected List<SaveFragmentBase> SaveFragments()
    {
        return GetComponents<IFragmentSaveable>().Select(e => e.Save()).ToList();
    }

    protected void LoadFragments(List<SaveFragmentBase> fragments)
    {
        var saveables = GetComponents<IFragmentSaveable>();
        var table = new Dictionary<string, IFragmentSaveable>();
        foreach (var saveable in saveables)
        {
            table[saveable.GetId()] = saveable;
        }

        foreach (var fragment in fragments)
        {
            if (table.ContainsKey(fragment.Id))
            {
                table[fragment.Id].Load(fragment);
            }
        }
    }
}
