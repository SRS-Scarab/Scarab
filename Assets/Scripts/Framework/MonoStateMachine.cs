#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonoStateMachine : MonoBehaviour
{
    [SerializeField]
    private List<MonoStateMachineBlackboard> blackboards = new();
    
    [SerializeField]
    private MonoState? state;

    private void Start()
    {
        if (state != null)
        {
            state.OnEnter(this);
        }
    }

    private void Update()
    {
        if (state != null)
        {
            state.OnTick(this, Time.deltaTime);
        }
    }

    public T? GetBlackboard<T>() where T : MonoStateMachineBlackboard
    {
        return (T?)blackboards.FirstOrDefault(e => e is T);
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
