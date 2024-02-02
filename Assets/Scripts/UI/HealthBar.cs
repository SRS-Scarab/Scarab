#nullable enable
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private CombatEntityVariable? entityVar;
    [SerializeField] private Slider? healthSlider;
    
    [Header("State")]
    
    [SerializeField] private CombatEntity? entity;

    public void Initialize(CombatEntity combatEntity) // can be utilized later when we add health bars for enemies
    {
        entity = combatEntity;
    }

    private void Update()
    {
        if (healthSlider != null)
        {
            if (entityVar != null && entityVar.Provide() != null) healthSlider.value = entityVar.Provide()!.GetHealth() / entityVar.Provide()!.GetMaxHealth();
            else if (entity != null) healthSlider.value = entity.GetHealth() / entity.GetMaxHealth();
        }
    }
}
