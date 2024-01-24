using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Generation/Types/Connection")]
public class GeneratedConnectionType : ScriptableObject
{
    public GeneratedConnectionType[] compatibleTypes;

    public bool IsCompatible(GeneratedConnectionType type)
    {
        return compatibleTypes.Contains(type);
    }
}
