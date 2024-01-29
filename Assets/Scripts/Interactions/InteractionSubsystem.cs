#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Subsystems/Interaction")]
public class InteractionSubsystem : ScriptableObject
{
    [SerializeField] private GameObjectVariable? playerVariable;
    [SerializeField] private List<Interactable> availableInteractables = new();

    public GameObject? GetPlayerObject() => playerVariable == null ? null : playerVariable.Provide();

    public Interactable? GetInteractable() => availableInteractables.OrderByDescending(e => e.interactionPriority).ThenBy(GetDistanceScore).FirstOrDefault();

    public void AddInteractable(Interactable interactable) => availableInteractables.Add(interactable);

    public void RemoveInteractable(Interactable interactable) => availableInteractables.Remove(interactable);

    private float GetDistanceScore(Interactable interactable) => playerVariable == null ? 0 : (playerVariable.Provide().transform.position - interactable.transform.position).sqrMagnitude;
}
