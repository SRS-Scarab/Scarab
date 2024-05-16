#nullable enable
using System;
using UnityEngine;

public class MonoStateMachine : MonoBehaviour
{
    [SerializeField]
    private MonoState? state;

    private void Start()
    {
        if (state != null) state.OnEnter(this);
    }

    private void Update()
    {
        if (state != null) state.OnTick(this, Time.deltaTime);
    }

    public MonoState? GetState()
    {
        return state;
    }

    public void SetState(MonoState? newState)
    {
        if (state != null) state.OnExit(this);
        state = newState;
        if (state != null) state.OnEnter(this);
    }
}
