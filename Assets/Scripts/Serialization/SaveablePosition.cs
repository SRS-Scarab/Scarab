#nullable enable
using UnityEngine;

public class SaveablePosition : MonoBehaviour, IFragmentSaveable
{
    private const string ID = "position";

    public string GetId()
    {
        return ID;
    }

    public SaveFragmentBase Save()
    {
        var position = transform.position;
        return new PositionFragmentV0(position.x, position.y, position.z, GetId());
    }

    public void Load(SaveFragmentBase fragment)
    {
        var latest = (PositionFragmentV0)fragment.GetLatest();
        transform.position = new Vector3(latest.X, latest.Y, latest.Z);
    }
}
