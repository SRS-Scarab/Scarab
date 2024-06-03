#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Prefab")]
public class AttackPrefab : ScriptableObject
{
    public bool isDetached;
    public AttackInstance? prefab;
    
    public bool TryInstantiate(CombatEntity source, Vector3 position, float rotation)
    {
        if (prefab == null) return false;

        var obj = Instantiate(prefab.gameObject, isDetached ? null : source.transform)!.GetComponent<AttackInstance>()!;
        obj.transform.position = position;
        obj.transform.localEulerAngles = new Vector3(0, rotation, 0);
        if (!obj.TryInitialize(source))
        {
            Destroy(obj.gameObject);
            return false;
        }
        return true;
    }
}
