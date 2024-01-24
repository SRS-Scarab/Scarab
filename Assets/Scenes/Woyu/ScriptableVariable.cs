using UnityEngine;

public abstract class ScriptableVariable<T> : ScriptableObject
{
    [SerializeField] public T value;
}

public abstract class VariableSetter<T> : MonoBehaviour
{
    [SerializeField] public ScriptableVariable<T> variable;
    [SerializeField] public T value;

    private void Awake()
    {
        variable.value = value;
    }
}
