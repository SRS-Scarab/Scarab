#nullable enable
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private CombatEntityVariable? entityVar;
    [SerializeField] private Slider? manaSlider;
    
    [Header("State")]
    
    [SerializeField] private CombatEntity? entity;

    public void Initialize(CombatEntity combatEntity) // can be utilized later when we add health bars for enemies
    {
        entity = combatEntity;
    }

    private void Update()
    {
        if (manaSlider != null)
        {
            if (entityVar != null && entityVar.Provide() != null) manaSlider.value = entityVar.Provide()!.GetMana() / entityVar.Provide()!.GetMaxMana();
            else if (entity != null) manaSlider.value = entity.GetMana() / entity.GetMaxMana();
        }
    }
}
