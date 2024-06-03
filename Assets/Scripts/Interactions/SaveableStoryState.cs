#nullable enable
using UnityEngine;

public class SaveableStoryState : MonoBehaviour, IFragmentSaveable
{
    [SerializeField]
    private string stateId = string.Empty;
    
    [SerializeField]
    private StoryStateVariable? stateVariable;

    public string GetId()
    {
        return stateId;
    }

    public SaveFragmentBase Save()
    {
        return new IntFragmentV0(stateVariable == null ? 0 : stateVariable.Provide(), GetId());
    }

    public void Load(SaveFragmentBase fragment)
    {
        var latest = (IntFragmentV0)fragment.GetLatest();
        if (stateVariable != null)
        {
            stateVariable.Consume(latest.Value);
        }
    }
}
