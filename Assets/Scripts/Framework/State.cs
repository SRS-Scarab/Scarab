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

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public abstract State Tick();
}

[Serializable]
public abstract class State<T> : State where T : StateMachine
{
    protected new T StateMachine => (T)base.StateMachine;

    protected State(T stateMachine) : base(stateMachine)
    {
    }
}
