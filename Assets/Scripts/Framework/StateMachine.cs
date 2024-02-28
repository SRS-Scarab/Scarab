#nullable enable
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State Current
    {
        get => current;
        set
        {
            current.Exit();
            current = value;
            current.Enter();
        }
    }
    
    [SerializeReference] private State current = null!;

    protected virtual void Start()
    {
        current = GetInitialState();
        current.Enter();
    }

    protected virtual void Update()
    {
        current = current.Tick();
    }

    protected abstract State GetInitialState();
}
