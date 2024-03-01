#nullable enable
using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public struct AttackInfo
{
    public float damage;
    public float knockback;
    public float persist;
    public bool isFree;
    public GameObject? hitboxes;
    public float indicate;
    public GameObject? indicator;

    public void Attack(CombatEntity source, Vector3 position, float rotation)
    {
        if (indicator != null)
        {
            var obj = Object.Instantiate(indicator, isFree ? null : source.transform);
            obj.transform.position = position;
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
            Object.Destroy(obj, indicate);
        }
        source.StartCoroutine(Instantiate(source, position, rotation));
    }

    private IEnumerator Instantiate(CombatEntity source, Vector3 position, float rotation)
    {
        yield return new WaitForSeconds(indicate);
        var obj = Object.Instantiate(hitboxes, isFree ? null : source.transform);
        if (obj != null)
        {
            obj.transform.position = position;
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
            var instance = obj.GetComponent<AttackInstance>();
            if (instance != null) instance.Initialize(source, this);
        }
    }
}
