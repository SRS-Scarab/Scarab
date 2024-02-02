#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Combat Entity")]
public class CombatEntityVariable : ScriptableVariable<CombatEntity?>
{
    [SerializeField] private CombatEntity? entity;
    
    public override CombatEntity? Provide() => entity;

    public override void Consume(CombatEntity? value) => entity = value;
}
