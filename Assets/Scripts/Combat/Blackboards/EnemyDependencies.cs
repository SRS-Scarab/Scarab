#nullable enable
using UnityEngine;
using UnityEngine.AI;

public class EnemyDependencies : Blackboard
{
    public CombatEntityVariable? playerEntityVar;

    public CombatEntity? entity;
    
    public NavMeshAgent? agent;

    public GameObject? attackPosition;
    
    public bool IsValid()
    {
        return playerEntityVar != null && playerEntityVar.Provide() != null &&
               entity != null &&
               agent != null &&
               attackPosition != null;
    }
}
