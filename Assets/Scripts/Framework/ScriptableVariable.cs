#nullable enable
using UnityEngine;

public abstract class ScriptableVariable<T> : ScriptableObject, IProvider<T>, IConsumer<T>
{
    public abstract T Provide();

    public abstract void Consume(T value);

    public static implicit operator T(ScriptableVariable<T> variable) => variable.Provide();
}
