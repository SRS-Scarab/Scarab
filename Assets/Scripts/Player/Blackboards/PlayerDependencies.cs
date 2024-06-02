#nullable enable
using UnityEngine;

public class PlayerDependencies : Blackboard
{
    public Actions? Actions => actionsVar == null ? null : actionsVar.Provide();
    
    public ActionsVariable? actionsVar;

    public new Rigidbody? rigidbody;

    public GroundChecker? groundChecker;

    public GameObject? feetPosition;

    public CombatEntity? entity;

    public TextureAnimator? animator;

    public InventorySubsystem? hotbarSubsystem;

    public InventoryVariable? hotbarVar;

    public CameraVariable? camVar;

    public bool IsValid()
    {
        return actionsVar != null &&
               rigidbody != null &&
               groundChecker != null &&
               feetPosition != null &&
               entity != null &&
               animator != null &&
               hotbarSubsystem != null &&
               hotbarVar != null &&
               camVar != null && camVar.Provide() != null;
    }
}