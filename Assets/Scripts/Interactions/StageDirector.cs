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
    [SerializeField] private Vector2 leftPosition = new(0.2f, 0.4f);
    [SerializeField] private Vector2 rightPosition = new(0.9f, 0.55f);
    [SerializeField] private Vector2 leftishPosition = new (0.35f, 0.55f);
    [SerializeField] private Vector2 rightishPosition = new (0.75f, 0.55f);

    [SerializeField] private StageActor[] actors = Array.Empty<StageActor>();
    [SerializeField] private StageDirection[] directions = Array.Empty<StageDirection>();

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
            instance.SetPosition(leftPosition);
            instance.SetOrientation(false);
        }
    }
    
    [YarnCommand("right-actor")]
    public void RightActor(string actorName)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null)
        {
            instance.SetPosition(rightPosition);
            instance.SetOrientation(true);
        }
    }

    [YarnCommand("leftish-actor")] // might make the screen too busy
    public void LeftishActor(string actorName)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null)
        {
            instance.SetPosition(leftishPosition);
            instance.SetOrientation(true);
        }
    }

    [YarnCommand("rightish-actor")] // might make the screen too busy
    public void RightishActor(string actorName)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null)
        {
            instance.SetPosition(rightishPosition);
            instance.SetOrientation(false);
        }
    }

    [YarnCommand("set-primary")]
    public void SetPrimary(string actorName)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null)
        {
            foreach (var actor in activeActors)
            {
                if(actor != instance) actor.FadeOut();
            }
            instance.FadeIn();
        }
    }
    
    [YarnCommand("no-primary")]
    public void NoPrimary()
    {
        foreach (var actor in activeActors) actor.FadeOut();
    }
    
    [YarnCommand("all-primary")]
    public void AllPrimary()
    {
        foreach (var actor in activeActors) actor.FadeIn();
    }
    
    [YarnCommand("set-inactive")]
    public void SetInactive(string actorName)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null) instance.FadeOut();
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
    
    [YarnCommand("set-expression")]
    public void SetExpression(string actorName, string expressionName)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null) instance.SetExpression(expressionName);
    }
    
    [YarnCommand("reset-expression")]
    public void ResetExpression(string actorName)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null) instance.ResetExpression();
    }
    
    [YarnCommand("perform-direction")]
    public void PerformDirection(string actorName, string directionName)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null)
        {
            var direction = directions.FirstOrDefault(e => e.directionName == directionName);
            if (direction != null) instance.PerformDirection(direction);
        }
    }
    
    [YarnCommand("reset-direction")]
    public void ResetDirection(string actorName)
    {
        var instance = GetActorInstance(actorName);
        if (instance != null) instance.ResetDirection();
    }

    private StageActorInstance? GetActorInstance(string actorName)
    {
        return activeActors.FirstOrDefault(e => e.GetActor() != null && e.GetActor()!.actorName == actorName);
    }
}
