#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn.Unity;

public class StageDirector : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private GameObject? actorPrefab;
    [SerializeField] private Vector2 leftSlot = new(0.25f, 0.5f);
    [SerializeField] private Vector2 rightSlot = new(0.75f, 0.5f);
    [SerializeField] private StageActor[] actors = Array.Empty<StageActor>();

    [Header("State")]
    
    [SerializeField] private List<StageActorInstance> activeActors = new();

    [YarnCommand("add-actor")]
    public void AddActor(string actorName)
    {
        if (GetActorInstance(actorName) != null) return;
        var actor = actors.FirstOrDefault(e => e.actorName == actorName);
        if (actor != null)
        {
            var obj = Instantiate(actorPrefab, transform);
            if (obj != null)
            {
                var instance = obj.GetComponent<StageActorInstance>();
                if (instance != null)
                {
                    instance.Initialize(actor);
                    activeActors.Add(instance);
                }
            }
        }
    }

    [YarnCommand("remove-actor")]
    public void RemoveActor(string actorName)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null)
        {
            Destroy(instance.gameObject);
            activeActors.Remove(instance);
        }
    }
    
    [YarnCommand("left-actor")]
    public void LeftActor(string actorName)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null)
        {
            instance.SetPosition(leftSlot);
            instance.SetOrientation(false);
        }
    }
    
    [YarnCommand("right-actor")]
    public void RightActor(string actorName)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null)
        {
            instance.SetPosition(rightSlot);
            instance.SetOrientation(true);
        }
    }
    
    [YarnCommand("reposition-actor")]
    public void Reposition(string actorName, float x, float y)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null) instance.SetPosition(new Vector2(x, y));
    }

    [YarnCommand("reorient-actor")]
    public void ReorientActor(string actorName, bool isFacingLeft)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null) instance.SetOrientation(isFacingLeft);
    }

    private StageActorInstance? GetActorInstance(string actorName)
    {
        return activeActors.FirstOrDefault(e => e.GetActor() != null && e.GetActor()!.actorName == actorName);
    }
}
