#nullable enable
using UnityEngine;

public class AttackInstance : MonoBehaviour
{
    public bool IsInitialized => source != null;
    
    public CombatEntity? Source => source;
    
    [SerializeField]
    private CombatEntity? source;

    [SerializeField]
    private AttackProcessor? first;

    public bool TryInitialize(CombatEntity newSource)
    {
        if (source != null) return false;
        source = newSource;
        return true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (source != null && first != null && other.attachedRigidbody != null)
        {
            var entity = other.attachedRigidbody.gameObject.GetComponent<CombatEntity>();
            if (entity != null)
            {
                first.Process(source, entity);
            }
        }
    }
}
