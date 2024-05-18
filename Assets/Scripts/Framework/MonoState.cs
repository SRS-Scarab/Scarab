#nullable enable
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoState : MonoBehaviour
{
    [SerializeField]
    private MonoState? parent;

    [SerializeField]
    private List<MonoState> children = new();

    private void Awake()
    {
        foreach (var child in children)
        {
            child.parent = this;
        }
    }

    public virtual void OnEnter(MonoStateMachine stateMachine)
    {
        OnEnterPropagate(stateMachine);
    }

    protected virtual void OnEnterPropagate(MonoStateMachine stateMachine)
    {
        if (parent != null)
        {
            parent.OnEnterPropagate(stateMachine);
        }
    }
    
    public virtual void OnTick(MonoStateMachine stateMachine, float delta)
    {
        OnTickPropagate(stateMachine, delta);
    }

    protected virtual void OnTickPropagate(MonoStateMachine stateMachine, float delta)
    {
        if (parent != null)
        {
            parent.OnTickPropagate(stateMachine, delta);
        }
    }

    public virtual void OnExit(MonoStateMachine stateMachine)
    {
        OnExitPropagate(stateMachine);
    }

    protected virtual void OnExitPropagate(MonoStateMachine stateMachine)
    {
        if (parent != null)
        {
            parent.OnExitPropagate(stateMachine);
        }
    }
}
