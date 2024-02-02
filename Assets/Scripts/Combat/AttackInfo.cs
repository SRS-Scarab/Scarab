#nullable enable
using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public struct AttackInfo
{
    public float damage;
    public float knockback;
    public float persist;
    public GameObject? hitboxes;

    public void Instantiate(CombatEntity source, Vector3 position, float rotation, bool isBound = true)
    {
        var obj = Object.Instantiate(hitboxes, isBound ? source.transform : null);
        if (obj != null)
        {
            obj.transform.position = position;
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
            var instance = obj.GetComponent<AttackInstance>();
            if (instance != null) instance.Initialize(source, this);
        }
    }
}
