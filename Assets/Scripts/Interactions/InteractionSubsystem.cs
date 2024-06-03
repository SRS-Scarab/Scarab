#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Subsystems/Interaction")]
public class InteractionSubsystem : ScriptableObject
{
    [SerializeField] private GameObjectVariable? playerVariable;
    [SerializeField] private List<Interactable> availableInteractables = new();

    public GameObject? GetPlayerObject()
    {
        return playerVariable == null ? null : playerVariable.Provide();
    }

    public Interactable? GetInteractable()
    {
        var filtered = availableInteractables.Where(e => e.gameObject.activeInHierarchy);
        var ordered = filtered.OrderByDescending(e => e.GetInteractionPriority()).ThenBy(GetDistanceScore);
        return ordered.FirstOrDefault();
    }

    public void AddInteractable(Interactable interactable)
    {
        availableInteractables.Add(interactable);
    }

    public void RemoveInteractable(Interactable interactable)
    {
        availableInteractables.Remove(interactable);
    }

    private float GetDistanceScore(Interactable interactable)
    {
        if (playerVariable == null || playerVariable.Provide() == null) return 0;
        return (playerVariable.Provide()!.transform.position - interactable.transform.position).sqrMagnitude;
    }
}
