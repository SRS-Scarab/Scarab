#nullable enable
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State Current
    {
        get => current;
        set
        {
            current.OnExit();
            current = value;
            current.OnEnter();
        }
    }
    
    [SerializeReference] private State current = null!;

    protected virtual void Start()
    {
        current = GetInitialState();
        current.OnEnter();
    }

    protected virtual void Update()
    {
        current.OnTick();
    }

    protected abstract State GetInitialState();
}
