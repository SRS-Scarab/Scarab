#nullable enable
using UnityEngine;

public abstract class MovementStateMachine : MonoBehaviour
{
    [SerializeReference] private MovementState current = null!;

    protected virtual void Start()
    {
        current = GetInitialState();
    }

    protected virtual void Update()
    {
        current = current.Tick();
    }

    protected abstract MovementState GetInitialState();

    protected void SetState(MovementState state)
    {
        current = state;
    }
}
