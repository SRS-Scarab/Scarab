#nullable enable
using UnityEngine.AI;

public class EnemyDependencies : Blackboard
{
    public CombatEntityVariable? playerEntityVar;

    public CombatEntity? entity;
    
    public NavMeshAgent? agent;
    
    public bool IsValid()
    {
        return playerEntityVar != null && playerEntityVar.Provide() != null &&
               entity != null &&
               agent != null;
    }
}
