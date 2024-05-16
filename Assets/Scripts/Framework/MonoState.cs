#nullable enable
using UnityEngine;

public abstract class MonoState : MonoBehaviour
{
    public virtual void OnEnter(MonoStateMachine stateMachine)
    {
    }

    public virtual void OnTick(MonoStateMachine stateMachine, float delta)
    {
    }

    public virtual void OnExit(MonoStateMachine stateMachine)
    {
    }
}
