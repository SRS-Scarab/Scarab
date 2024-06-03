#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Attack Proxy")]
public class AttackProxy : ScriptableObject
{
    public PlayerActionState? actionState;
    public PlayerSpecialState? specialState;

    public float GetActionRechargeProgress()
    {
        return actionState == null ? 0 : actionState.GetRechargeProgress();
    }
    
    public float GetSpecialRechargeProgress()
    {
        return specialState == null ? 0 : specialState.GetRechargeProgress();
    }
}
