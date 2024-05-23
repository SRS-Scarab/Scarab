#nullable enable
using UnityEngine;

public class PlayerDependencies : Blackboard
{
    public Actions? Actions => actionsVar == null ? null : actionsVar.Provide();
    
    public ActionsVariable? actionsVar;

    public new Rigidbody? rigidbody;

    public GroundChecker? groundChecker;

    public CombatEntity? entity;

    public InventorySubsystem? hotbarSubsystem;

    public InventoryVariable? hotbarVar;

    public CameraVariable? camVar;

    public bool IsValid()
    {
        return actionsVar != null &&
               rigidbody != null &&
               groundChecker != null &&
               entity != null &&
               hotbarSubsystem != null &&
               hotbarVar != null &&
               camVar != null && camVar.Provide() != null;
    }
}