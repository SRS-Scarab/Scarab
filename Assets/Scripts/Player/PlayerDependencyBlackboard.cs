#nullable enable
using UnityEngine;

public class PlayerDependencyBlackboard : MonoStateMachineBlackboard
{
    public Actions? Actions => actionsVar == null ? null : actionsVar.Provide();
    
    public ActionsVariable? actionsVar;

    public new Rigidbody? rigidbody;

    public GroundChecker? groundChecker;

    public bool IsValid()
    {
        return actionsVar != null && rigidbody != null && groundChecker != null;
    }
}