#nullable enable
using System;
using UnityEngine;

[Serializable]
public abstract class MovementState
{
    protected MovementStateMachine StateMachine => stateMachine;
    [SerializeField] private MovementStateMachine stateMachine;

    protected MovementState(MovementStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public abstract MovementState Tick();
}

[Serializable]
public abstract class MovementState<T> : MovementState where T : MovementStateMachine
{
    protected new T StateMachine => (T)base.StateMachine;

    protected MovementState(T stateMachine) : base(stateMachine)
    {
    }
}
