#nullable enable
using UnityEngine;

public abstract class VariableSetter<T> : MonoBehaviour
{
    [SerializeField] public ScriptableVariable<T?>? variable;
    [SerializeField] public T? value;

    private void Awake()
    {
        if(variable != null) variable.Consume(value);
    }
}