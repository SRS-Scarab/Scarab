#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SaveableObject : MonoBehaviour
{
    [SerializeField]
    private bool isLoaded;

    public void ResetIsLoaded() => isLoaded = false;

    public bool IsLoaded() => isLoaded;
    
    public abstract SaveObjectBase Save();

    public virtual void Load(SaveObjectBase obj)
    {
        isLoaded = true;
    }

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
