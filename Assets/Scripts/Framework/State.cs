#nullable enable
using System;
using UnityEngine;

[Serializable]
public abstract class State
{
    protected StateMachine StateMachine => stateMachine;
    [SerializeField] private StateMachine stateMachine;

    protected State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }

    public abstract void OnTick();
}

[Serializable]
public abstract class State<T> : State where T : StateMachine
{
    protected new T StateMachine => (T)base.StateMachine;

    protected State(T stateMachine) : base(stateMachine)
    {
    }
}
