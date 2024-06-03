#nullable enable
using UnityEngine;
using UnityEngine.UI;

public class AttackCooldowns : MonoBehaviour
{
    [SerializeField]
    private Image? actionImage;

    [SerializeField]
    private Image? specialImage;
    
    [SerializeField]
    private AttackProxy? proxy;

    private void Update()
    {
        if (proxy != null)
        {
            if (actionImage != null)
            {
                actionImage.fillAmount = proxy.GetActionRechargeProgress();
            }
        
            if (specialImage != null)
            {
                specialImage.fillAmount = proxy.GetSpecialRechargeProgress();
            }
        }
    }
}
